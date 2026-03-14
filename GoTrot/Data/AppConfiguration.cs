using Microsoft.Extensions.Configuration;
using System.IO;

namespace GoTrot.Data
{
    /// <summary>
    /// Čita konfiguraciju iz appsettings.json.
    /// Connection string više nije hardkodiran u kodu.
    /// </summary>
    public static class AppConfiguration
    {
        private static IConfiguration? _config;

        public static IConfiguration Config
        {
            get
            {
                if (_config == null)
                {
                    _config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                        .Build();
                }
                return _config;
            }
        }

        public static string ConnectionString =>
            Config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' nije pronađen u appsettings.json");
    }
}
