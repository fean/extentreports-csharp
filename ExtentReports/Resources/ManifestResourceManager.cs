using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace AventStack.ExtentReports.Resources
{
    public static class ManifestResourceManager
    {
        public static string GetString(string manifestResource)
        {
            using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(manifestResource))
            {
                if (resourceStream is null)
                {
                    throw new InvalidOperationException($"The requested resource {manifestResource} is not available in the assembly.");
                }
                    
                using (var reader = new StreamReader(resourceStream))
                {
                    return reader.ReadToEnd();
                }
                    
            }
        }

        public static TValue GetJson<TValue>(string manifestResource)
        {
            var jsonContent = GetString(manifestResource);

            return JsonConvert.DeserializeObject<TValue>(jsonContent);
        }
    }
}