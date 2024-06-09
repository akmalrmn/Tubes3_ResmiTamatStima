using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Transactions;

namespace Tubes3_ResmiTamatStima.Data
{
    public static class DBUtilities
    {
        // Initializes the database with BMP file paths from a test directory
        public static async Task<bool> InitializeDBAsync(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            // check if table already exist using connection
            
            await connection.ExecuteAsync("DROP TABLE IF EXISTS biodata;");
            await connection.ExecuteAsync("DROP TABLE IF EXISTS sidik_jari;");

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

            using var transaction = await connection.BeginTransactionAsync();

                // Execute the creation of tables
                await connection.ExecuteAsync(createSQL, transaction: transaction);
            System.Diagnostics.Debug.WriteLine(createSQL);


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
                    new { imageData = "test/" + relativePath, name = "Christian Justin Hendrawan" + idx });
                System.Diagnostics.Debug.WriteLine("test/" + relativePath);
            }
            await transaction.CommitAsync();
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

        // Retrieves all image data (berkas_citra) from the sidik_jari table
        public static async Task<List<string>> GetDataFromDB(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            var data = await connection.QueryAsync<string>("SELECT berkas_citra FROM sidik_jari;");
            return data.ToList();
        }

        // Retrieves names associated with a given image data (citra) from the sidik_jari table
        public static async Task<string> GetNamesByCitraFromDB(IConfiguration configuration, string citra)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            var data = await connection.QueryAsync<string>("SELECT nama FROM sidik_jari WHERE berkas_citra = @citra;", new { citra });

            return string.Join(", ", data);
        }

        // Retrieves all names from the biodata table
        public static async Task<List<string>> GetNamesFromBiodata(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            var data = await connection.QueryAsync<string>("SELECT nama FROM biodata;");
            return data.ToList();
        }

        // Retrieves biodata of a person by their name from the biodata table
        public static async Task<Biodata> GetBiodataByNameFromDB(IConfiguration configuration, string name)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            var data = await connection.QueryFirstOrDefaultAsync<Biodata>("SELECT * FROM biodata WHERE nama = @name;", new { name });

            return data;
        }

        // Updates the name in the sidik_jari table
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

        // Insert dummy data into the biodata table
        public static async Task InsertDummyDataAsync(IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            var insertSQL = @"
    INSERT INTO `biodata` (`NIK`, `nama`, `tempat_lahir`, `tanggal_lahir`, `jenis_kelamin`, `golongan_darah`, `alamat`, `agama`, `status_perkawinan`, `pekerjaan`, `kewarganegaraan`) VALUES
    ('1234567890123456', 'j0k0w1', 'Surabaya', '1990-01-01', 'Laki-laki', 'A', 'Jl. Mawar No. 1', 'Islam', 'Belum Menikah', 'Programmer', 'Indonesia'),
    ('2345678901234567', 'd3w1', 'Jakarta', '1992-02-02', 'Perempuan', 'B', 'Jl. Melati No. 2', 'Kristen', 'Belum Menikah', 'Desainer', 'Indonesia'),
    ('3456789012345678', '4n4', 'Bandung', '1994-03-03', 'Perempuan', 'O', 'Jl. Anggrek No. 3', 'Hindu', 'Belum Menikah', 'Dokter', 'Indonesia'),
    ('4567890123456789', 'b4mb4ng', 'Medan', '1988-04-04', 'Laki-laki', 'AB', 'Jl. Kenanga No. 4', 'Budha', 'Belum Menikah', 'Guru', 'Indonesia'),
    ('5678901234567890', 'r4h4y5', 'Yogyakarta', '1985-05-05', 'Perempuan', 'A', 'Jl. Tulip No. 5', 'Katolik', 'Belum Menikah', 'Wiraswasta', 'Indonesia'),
    ('6789012345678901', 'yud17', 'Semarang', '1987-06-06', 'Laki-laki', 'B', 'Jl. Flamboyan No. 6', 'Islam', 'Belum Menikah', 'Pengacara', 'Indonesia'),
    ('7890123456789012', 'p3t3r', 'Palembang', '1991-07-07', 'Laki-laki', 'O', 'Jl. Mawar No. 7', 'Kristen', 'Belum Menikah', 'Dokter Gigi', 'Indonesia'),
    ('8901234567890123', 'd3s7', 'Makassar', '1993-08-08', 'Perempuan', 'AB', 'Jl. Melati No. 8', 'Hindu', 'Belum Menikah', 'Arsitek', 'Indonesia'),
    ('9012345678901234', 'b3n7', 'Bali', '1995-09-09', 'Laki-laki', 'A', 'Jl. Anggrek No. 9', 'Budha', 'Belum Menikah', 'Pilot', 'Indonesia'),
    ('0123456789012345', 'g4l4ng', 'Bogor', '1986-10-10', 'Laki-laki', 'B', 'Jl. Kenanga No. 10', 'Katolik', 'Belum Menikah', 'Penyanyi', 'Indonesia'),
    ('1234509876543210', 'k3k3', 'Malang', '1989-11-11', 'Perempuan', 'O', 'Jl. Tulip No. 11', 'Islam', 'Belum Menikah', 'Penulis', 'Indonesia'),
    ('2345609876543210', 'c4ndy', 'Balikpapan', '1990-12-12', 'Perempuan', 'AB', 'Jl. Flamboyan No. 12', 'Kristen', 'Belum Menikah', 'Model', 'Indonesia'),
    ('3456709876543210', 'p4ndu', 'Manado', '1993-01-13', 'Laki-laki', 'A', 'Jl. Mawar No. 13', 'Hindu', 'Duda', 'Belum Menikah', 'Indonesia'),
    ('4567809876543210', 'b0y', 'Samarinda', '1992-02-14', 'Laki-laki', 'B', 'Jl. Melati No. 14', 'Budha', 'Belum Menikah', 'Atlet', 'Indonesia'),
    ('5678909876543210', 'd4n1', 'Denpasar', '1988-03-15', 'Laki-laki', 'O', 'Jl. Anggrek No. 15', 'Katolik', 'Belum Menikah', 'Polisi', 'Indonesia'),
    ('6789009876543210', '4d1t', 'Solo', '1987-04-16', 'Laki-laki', 'AB', 'Jl. Kenanga No. 16', 'Islam', 'Belum Menikah', 'Tentara', 'Indonesia');
    ";

            using var transaction = await connection.BeginTransactionAsync();

            try
            {
                // Execute the insertions
                await connection.ExecuteAsync(insertSQL, transaction: transaction);

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
