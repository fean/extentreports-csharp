using System;
using System.Collections.Generic;

namespace AventStack.ExtentReports.Gherkin
{
    internal static class GherkinDialectProvider
    {
        private static readonly Lazy<Dictionary<string, GherkinKeywords>> _lazyDialects = new Lazy<Dictionary<string,GherkinKeywords>>(Resources.GherkinLangs.GetLanguages);
        private static string _dialect;

        private static Dictionary<string, GherkinKeywords> Dialects
            => _lazyDialects.Value;

        public static string DefaultDialect { get; } = "en";

        public static GherkinDialect Dialect { get; private set; }

        public static string Language
        {
            get
            {
                return string.IsNullOrEmpty(_dialect)
                    ? _dialect = DefaultDialect
                    : _dialect;   
            }
            set
            {
                var isKnownDialect = Dialects.TryGetValue(value, out var keywords);

                if (!isKnownDialect)
                {
                    throw new InvalidGherkinLanguageException($"{value} is not a valid Gherkin dialect");
                }

                _dialect = value;
                Dialect = new GherkinDialect(_dialect, keywords);
            }
        }
    }
}
