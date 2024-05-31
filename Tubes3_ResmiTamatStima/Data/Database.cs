using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Tubes3_ResmiTamatStima.Data
{
    public static class DBUtilities
    {
        public static async Task<bool> InitializeDBAsync(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var createSQL = @"
            CREATE TABLE IF NOT EXISTS `biodata` (
                `NIK` TEXT PRIMARY KEY,
                `nama` TEXT,
                `tempat_lahir` TEXT,
                `tanggal_lahir` TEXT,
                `jenis_kelamin` TEXT,
                `golongan_darah` TEXT,
                `alamat` TEXT,
                `agama` TEXT,
                `status_perkawinan` TEXT,
                `pekerjaan` TEXT,
                `kewarganegaraan` TEXT
            );

            CREATE TABLE IF NOT EXISTS `sidik_jari` (
                `berkas_citra` TEXT,
                `nama` TEXT
            );";

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            using var transaction = await connection.BeginTransactionAsync();

            try
            {
                // Execute the creation of tables
                await connection.ExecuteAsync(createSQL, transaction: transaction);

                // Get all BMP files from the specified directory
                string[] files = Directory.GetFiles(@"../data", "*.bmp");
                int idx = 0;

                foreach (string file in files)
                {
                    idx++;
                    using Bitmap image = new Bitmap(file);

                    // Ensure the image is large enough for the specified portion
                    if (image.Width < 30 || image.Height < 30)
                    {
                        throw new ArgumentException("Image is too small for the specified portion size.");
                    }

                    // Extract a 30x30 portion of the image
                    using Bitmap portion = image.Clone(new Rectangle(0, 0, 30, 30), image.PixelFormat);

                    // Convert the portion to binary data
                    byte[] binaryData = ConvertImageToBinary(portion);

                    // Convert binary data to Base64 string representation
                    string base64Data = Convert.ToBase64String(binaryData);

                    // Insert the converted data into the database
                    await connection.ExecuteAsync("INSERT INTO sidik_jari (berkas_citra, nama) VALUES (@imageData, @name)",
                        new { imageData = base64Data, name = "Jokowi" + idx }, transaction: transaction);
                }

                // Commit the transaction if everything is successful
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Rollback the transaction if an error occurs
                await transaction.RollbackAsync();
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public static async Task TestDBAsync(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            // Check if the 'biodata' table exists
            var biodataExists = await connection.ExecuteScalarAsync<int>("SELECT count(*) FROM sqlite_master WHERE type='table' AND name='biodata';");
            System.Diagnostics.Debug.WriteLine($"Biodata table exists: {biodataExists > 0}");

            // Check if the 'sidik_jari' table exists
            var sidikJariExists = await connection.ExecuteScalarAsync<int>("SELECT count(*) FROM sqlite_master WHERE type='table' AND name='sidik_jari';");
            System.Diagnostics.Debug.WriteLine($"Sidik Jari table exists: {sidikJariExists > 0}");

            // Check the number of records in the 'sidik_jari' table
            var sidikJariCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM sidik_jari;");
            System.Diagnostics.Debug.WriteLine($"Number of records in Sidik Jari table: {sidikJariCount}");
        }

        public static async Task InitializeAndTestDBAsync(IConfiguration configuration)
        {
            await InitializeDBAsync(configuration);
            await Task.Delay(1000);
            await TestDBAsync(configuration);
        }

        // Converts a Bitmap to a binary representation
        static byte[] ConvertImageToBinary(Bitmap image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return ms.ToArray();
            }
        }
    }
}
