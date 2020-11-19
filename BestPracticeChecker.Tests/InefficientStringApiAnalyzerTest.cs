using Microsoft.CodeAnalysis;
using System.Threading.Tasks;
using BestPracticeChecker.Resources;
using Xunit;

namespace BestPracticeChecker.Tests
{
    public class InefficientStringApiAnalyzerTest : BaseDiagnosticVerifierTest<InefficientStringApiAnalyzer>
    {

        [Fact]
        public async Task StringEndsWithHighlighted()
        {
            const string test = @"
using System;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomething(string a)
        {
            String b = ""c"";
            b.EndsWith(a);
        }
    } 
}";

            var expected = new DiagnosticResult("BP0007", DiagnosticSeverity.Warning)
                .WithLocation(11, 13)
                .WithMessage(DiagnosticStrings.GetString("InefficientStringApiMessageFormat").ToString());
            await VerifyCSharpDiagnosticAsync(test, expected);
        }


        [Fact]
        public async Task StringStartsWithHighlighted()
        {
            const string test = @"
using System;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomething(string a)
        {
            String b = ""c"";
            b.StartsWith(a);
        }
    } 
}";
            var expected = new DiagnosticResult("BP0007", DiagnosticSeverity.Warning)
                .WithLocation(11, 13)
                .WithMessage(DiagnosticStrings.GetString("InefficientStringApiMessageFormat").ToString());
            await VerifyCSharpDiagnosticAsync(test, expected);
        }

        [Fact]
        public async Task StringStartsWithOrdinalNoHighlighted()
        {
            const string test = @"
using System;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomething(string a, StringComparison ordinal)
        {
            String b = ""c"";
            b.StartsWith(a, StringComparison.Ordinal);
        }
    } 
}";
            await VerifyCSharpDiagnosticAsync(test);
        }
    }
}
