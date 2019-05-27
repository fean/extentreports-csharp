using System.Collections.Generic;
using AventStack.ExtentReports.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace AventStack.ExtentReports.Reporter
{
    public abstract class ConfigurableReporter : AbstractReporter
    {
        public ConfigurationManager MasterConfig { get; protected internal set; } = new ConfigurationManager();

        public void LoadConfig(string filePath)
        {
            CheckFileExistence(filePath);

            var xdoc = XDocument.Load(filePath, LoadOptions.None);
            if (xdoc == null)
            {
                throw new FileLoadException("Unable to configure report with the supplied configuration. Please check the input file and try again.");
            }

            LoadConfigFileContents(xdoc);
        }

        public void LoadJsonConfig(string filePath)
        {
            CheckFileExistence(filePath);

            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(filePath));

            foreach (var configOption in result)
            {
                MasterConfig.AddConfig(configOption.Key, configOption.Value);
            }
        }

        private void CheckFileExistence(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file \"{filePath}\" was not found.");
            }
        }
        
        private void LoadConfigFileContents(XDocument xdoc)
        {
            var configElements = xdoc.Descendants("configuration").First().Elements();
            
            foreach (var element in configElements)
            {
                MasterConfig.AddConfig(element.Name.ToString(), element.Value);
            }
        }
    }
}
