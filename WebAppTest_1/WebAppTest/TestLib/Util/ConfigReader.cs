using Microsoft.Extensions.Configuration;
using System.IO;

namespace TestLib.Util
{
    public class ConfigReader
    {
        #region "Attributes"
        private readonly IConfigurationRoot builder;
        #endregion "Attributes"

        #region "Constructor"
        public ConfigReader()
        {
            builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile($"appsettings.json")
                 .Build();
        }
        #endregion "Constructor"

        #region "Methods"
        public string GetConfig(string pathConfig)
        {
            return builder.GetSection(pathConfig).Value;
        }
        #endregion "Methods"
    }
}
