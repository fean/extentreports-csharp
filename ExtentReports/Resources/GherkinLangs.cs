using System.Collections.Generic;
using AventStack.ExtentReports.Gherkin;

namespace AventStack.ExtentReports.Resources
{
    internal static class GherkinLangs
    {

        internal static Dictionary<string, GherkinKeywords> GetLanguages()
            => ManifestResourceManager.GetJson<Dictionary<string, GherkinKeywords>>(
                "AventStack.ExtentReports.Resources.GherkinLangs.txt");
    }
}