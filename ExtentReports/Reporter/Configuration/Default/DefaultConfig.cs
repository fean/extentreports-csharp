using System.Collections.Generic;
using AventStack.ExtentReports.Configuration;

namespace AventStack.ExtentReports.Reporter.Configuration
{
    public static class DefaultConfig
    {
        private static Dictionary<string, string> _configOptions = new Dictionary<string, string>()
        {
            { "encoding", "utf-8" },
            { "protocol", "https" },
            { "theme", "standard" },
            { "documentTitle", "Extent Framework" },
            { "reportName", "Extent Framework" },
            { "js", "" },
            { "css", "" },
            { "enableCategoryView", "true" },
            { "enableExceptionView", "true" },
            { "enableAuthorView", "true" },
            { "enableTestRunnerLogsView", "true" },
            { "enableTimeline", "true" },
            { "chartVisibleOnOpen", "true" }
        };

        public static void InitializeManager(ConfigurationManager manager)
        {
            foreach (var option in _configOptions)
            {
                manager.AddConfig(option.Key, option.Value);
            }
        }
    }
}