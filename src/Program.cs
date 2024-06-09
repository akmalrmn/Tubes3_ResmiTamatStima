using Microsoft.Extensions.Configuration;
using Tubes3_ResmiTamatStima.Data;

namespace Tubes3_ResmiTamatStima
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Task.Run(async () => await DBUtilities.InitializeDBAsync(configuration)).Wait();
            InitializeAndRun(configuration);
        }
        
        private static void InitializeAndRun(IConfiguration configuration)
        {
            Application.Run(new Form1(configuration));
        }
    }
}