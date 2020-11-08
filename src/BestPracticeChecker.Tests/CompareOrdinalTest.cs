using Microsoft.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace BestPracticeChecker.Tests
{
    public class CompareOrdinalTest : BaseDiagnosticVerifierTest<CompareOrdinalAnalyzer>
    {
        [Fact]
        public async Task StringEqualsHighlighted()
        {
            const string test = @"
using System;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomething(string a, string b) 
        { 
           if(String.Equals(a,b))
                return;
        }
    } 
}";

            var expected = new DiagnosticResult("BP0006", DiagnosticSeverity.Warning)
            .WithLocation(10, 29)
            .WithMessage("Verwende effiziente Methoden zum vergleichen von Strings wie String.CompareOrdinal() oder eine mittels StringComparison überladene String.Equals()-Methode");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }


        [Fact]
        public async Task StringCompareHighlighted()
        {
            const string test = @"
using System;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomething(string a, string b) 
        { 
           int c = String.Compare(a,b);
        }
    } 
}";

            var expected = new DiagnosticResult("BP0006", DiagnosticSeverity.Warning)
                .WithLocation(10, 35)
                .WithMessage("Verwende effiziente Methoden zum vergleichen von Strings wie String.CompareOrdinal() oder eine mittels StringComparison überladene String.Equals()-Methode");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }

        [Fact]
        public async Task StringCompareOrdinalNoHighlighted()
        {
            const string test = @"
using System;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomething(string a, string b) 
        { 
           int c = String.CompareOrdinal(a,b);
        }
    } 
}";
             await VerifyCSharpDiagnosticAsync(test);
        }

        // Check for String.Equals Overload

        [Fact]
        public async Task StringEqualsOverloadNoHighlighted()
        {
            const string test = @"
using System;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomething(string a, string b) 
        { 
            if(String.Equals(a,b, StringComparison.Ordinal))
                return;
        }
    } 
}";

            await VerifyCSharpDiagnosticAsync(test);
        }
    }
}

