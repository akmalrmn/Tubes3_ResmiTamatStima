using Microsoft.Extensions.Configuration;
using System.IO;
using Tubes3_ResmiTamatStima.Data;

namespace Tubes3_ResmiTamatStima
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            DBUtilities.InitializeAndTestDBAsync(configuration).GetAwaiter().GetResult();


            Application.Run(new Form1());
        }
    }
}