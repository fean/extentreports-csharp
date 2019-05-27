using System.Collections.Generic;
using System.Linq;

namespace AventStack.ExtentReports.Configuration
{
    public class ConfigurationManager
    {
        private Dictionary<string, string> Configuration { get; } = new Dictionary<string, string>();
        
        public string GetValue(string k)
        {
            var hasConfig = Configuration.TryGetValue(k, out var config);

            return hasConfig
                ? config
                : null;
        }
        
        public void AddConfig(string key, string value) {
            Configuration.Add(key, value);
        }

        private bool ContainsConfig(string key)
        {
            return Configuration.ContainsKey(key);
        }

        private void RemoveConfig(string key)
        {
            Configuration.Remove(key);
        }
    }
}
