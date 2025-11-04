using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


namespace PetStoreAPITest.Utils
{
    public static class ConfigManager
    {
        private static readonly IConfigurationRoot _config;
        
        static ConfigManager()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string BaseUrl => _config["BaseUrl"] ?? string.Empty;
        public static string ApiKey => _config["ApiKey"] ?? string.Empty;
    }
}
