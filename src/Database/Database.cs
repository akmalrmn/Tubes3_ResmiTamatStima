using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Transactions;

namespace Tubes3_ResmiTamatStima.Data
{
    public static class DBUtilities
    {
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
    }
}
