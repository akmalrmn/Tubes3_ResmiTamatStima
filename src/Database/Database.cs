using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Transactions;

namespace Tubes3_ResmiTamatStima.Data
{
    public static class DBUtilities
    {

        public static async Task<bool> InitializeDBAsync(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            // ... rest of your code ...

            // Get the path to the test directory
            string testPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../test"));
            System.Diagnostics.Debug.WriteLine($"Looking for BMP files in: {testPath}");

            // Get all BMP files from the test directory
            string[] files = Directory.GetFiles(testPath, "*.bmp");

            int idx = 0;
            foreach (string file in files)
            {
                idx++;

                // Get the relative path to the file
                string relativePath = Path.GetRelativePath(testPath, file);

                // Insert the relative path into the database
                await connection.ExecuteAsync("INSERT INTO sidik_jari (berkas_citra, nama) VALUES (@imageData, @name)",
                    new { imageData = "test/" + relativePath, name = "Jokowi" });
                System.Diagnostics.Debug.WriteLine("test/" + relativePath); 
            }

            return true;
        }

        // Converts a Bitmap to a binary representation
        public static byte[] ConvertImageToBinary(Bitmap image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return ms.ToArray();
            }
        }
        public static async Task<List<string>> GetDataFromDB(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            var data = await connection.QueryAsync<string>("SELECT berkas_citra FROM sidik_jari;");
            return data.ToList();
        }

        public static async Task<string> GetNamesByCitraFromDB(IConfiguration configuration, string citra)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            var data = await connection.QueryAsync<string>("SELECT nama FROM sidik_jari WHERE berkas_citra = @citra;", new { citra });

            return string.Join(", ", data);
        }

        public static async Task<List<string>> GetNamesFromBiodata(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            var data = await connection.QueryAsync<string>("SELECT nama FROM biodata;");
            return data.ToList();
        }

        public static async Task<Biodata> GetBiodataByNameFromDB(IConfiguration configuration, string name)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            var data = await connection.QueryFirstOrDefaultAsync<Biodata>("SELECT * FROM biodata WHERE nama = @name;", new { name });
            
            return data;
        }

        public static async Task UpdateNameInSidikJariAsync(IConfiguration configuration, string oldName, string newName)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            var updateSQL = @"
    UPDATE sidik_jari
    SET nama = @newName
    WHERE nama = @oldName;
    ";

            using var transaction = await connection.BeginTransactionAsync();

            try
            {
                // Execute the update
                var affectedRows = await connection.ExecuteAsync(updateSQL, new { newName, oldName }, transaction: transaction);

                if (affectedRows > 0)
                {
                    Console.WriteLine($"Successfully updated {affectedRows} record(s).");
                }
                else
                {
                    Console.WriteLine("No records were updated.");
                }

                // Commit the transaction if everything is successful
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Rollback the transaction if an error occurs
                await transaction.RollbackAsync();
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}
