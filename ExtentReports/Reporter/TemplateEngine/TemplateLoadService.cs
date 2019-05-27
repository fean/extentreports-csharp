using AventStack.ExtentReports.Views;
using RazorEngine.Templating;
using AventStack.ExtentReports.Resources;

namespace AventStack.ExtentReports.Reporter.TemplateEngine
{
    internal static class TemplateLoadService
    {
        private static string GetCleanName(string templateName)
        {
            var arr = templateName.Split('.');
            
            return arr.Length > 1
                ? arr[arr.Length - 1]
                : arr[0];
        }
        
        public static void LoadTemplate<TMarker>(string template) where TMarker : IViewsMarker
        {
            LoadTemplate<TMarker>(new[] { template });
        }

        public static void LoadTemplate<TMarker>(string[] templateNames) where TMarker : IViewsMarker
        {
            var markerType = typeof(TMarker);
            
            foreach (var templateName in templateNames)
            {
                var resourceName = $"{markerType.Namespace}.{templateName}.cshtml";
                var template = ManifestResourceManager.GetString(resourceName);
                var name = GetCleanName(templateName);
                
                RazorEngineManager.Instance.Razor.AddTemplate(name, template);
            }
        }
    }
}
