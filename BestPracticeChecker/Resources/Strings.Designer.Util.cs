using System;
using Microsoft.CodeAnalysis;

namespace BestPracticeChecker.Resources
{
    public static class DiagnosticStrings
    {
        public static LocalizableString GetString(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            return new LocalizableResourceString(name, Strings.ResourceManager, typeof(Strings));
        }

        public static string GetHelpLinkUri(string file) =>
            $"https://github.com/emanuelbuholzer/unity-best-practices/tree/master/docs/reference/{file}";
        
        public static class DiagnosticCategory
        {
            public static readonly string Performance = Strings.CategoryPerformance;
            public static readonly string Correctness = Strings.CategoryCorrectness;
            public static readonly string TypeSafety = Strings.CategoryTypeSafety;
            public static readonly string CodeStyle = Strings.CategoryCodeStyle;
        }
    }
}