using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AventStack.ExtentReports.Gherkin.Model
{
    public static class GherkinModelActivator
    {
        private delegate object Activator();
        private static readonly Dictionary<string, Activator> Activators = new Dictionary<string, Activator>();

        public static IGherkinFormatterModel ActivateKeywordModel(string keyword)
        {
            var hasActivator = Activators.TryGetValue(keyword, out var activator);

            if (!hasActivator)
            {
                var gherkinType = Assembly.GetExecutingAssembly().GetTypes()
                    .First(type => type.Name.Equals(keyword, StringComparison.CurrentCultureIgnoreCase));
                
                Activators.Add(keyword, activator = CreateActivator(gherkinType));
            }

            return (IGherkinFormatterModel) activator.Invoke();
        }

        private static Activator CreateActivator(Type gherkinType)
        {
            var constructor = gherkinType.GetConstructors().First();
            var newExp = Expression.New(constructor);
            var lambda = Expression.Lambda(typeof(Activator), newExp);
            
            return (Activator)lambda.Compile();
        }
    }
}