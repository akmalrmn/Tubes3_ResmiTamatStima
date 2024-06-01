using Microsoft.Extensions.Configuration;
using System.IO;
using Tubes3_ResmiTamatStima.Data;
using Tubes3_ResmiTamatStima.Algorithms;
using System.Threading.Tasks;

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

            InitializeAndRun(configuration);
        }

        private static void InitializeAndRun(IConfiguration configuration)
        {
            Application.Run(new Form1(configuration));
        }
    }
}