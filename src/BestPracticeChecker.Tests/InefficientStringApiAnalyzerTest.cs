using Microsoft.CodeAnalysis;
using System.Threading.Tasks;
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

            var expected = new DiagnosticResult("InefficientStringApi", DiagnosticSeverity.Warning)
                .WithLocation(11, 24)
                .WithMessage("Use String.StartsWith and String.EndsWith with custommethods");
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
            var expected = new DiagnosticResult("InefficientStringApi", DiagnosticSeverity.Warning)
                .WithLocation(11, 26)
                .WithMessage("Use String.StartsWith and String.EndsWith with custommethods");
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
