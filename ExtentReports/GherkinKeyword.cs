using AventStack.ExtentReports.Core;
using AventStack.ExtentReports.Gherkin;
using AventStack.ExtentReports.Gherkin.Model;

using System;
using System.Linq;
using System.Reflection;

namespace AventStack.ExtentReports
{
    public class GherkinKeyword
    {
        public IGherkinFormatterModel Model { get; private set; }

        public GherkinKeyword(string keyword)
        {
            var language = GherkinDialectProvider.Language;
            var dialect = GherkinDialectProvider.Dialect;
            
            var apiKeyword = keyword.First().ToString().ToUpper() + keyword.Substring(1);
            apiKeyword = apiKeyword.Equals("*") ? Asterisk.GherkinName : apiKeyword;

            try
            {
                var isDefaultLanguage = language.Equals(GherkinDialectProvider.DefaultDialect,
                    StringComparison.OrdinalIgnoreCase);
                
                if (!isDefaultLanguage)
                {
                    apiKeyword = dialect.Match(keyword);
                }

                if (apiKeyword == null)
                {
                    throw new GherkinKeywordNotFoundException($"Keyword {apiKeyword} cannot be null");
                }

                Model = GherkinModelActivator.ActivateKeywordModel(apiKeyword);
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException($"Invalid keyword specified: {keyword}", e);
            }
        }
    }
}
