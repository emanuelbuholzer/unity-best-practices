using System.Threading.Tasks;
using BestPracticeChecker;
using Microsoft.CodeAnalysis;
using Xunit;

namespace BestPracticeChecker.Tests
{
	public class ConditionalDebugAnalyzerTest : BaseDiagnosticVerifierTest<ConditionalDebugAnalyzer>
	{

		[Fact]
		public async Task VerifyConditionalDebugWorks()
		{
			const string test = @"
using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomething(string message)
        {
            Debug.Log(message); 
        }
    } 
}";

			var expected = new DiagnosticResult("ConditionalDebug", DiagnosticSeverity.Warning)
				.WithLocation(10, 13)
				.WithMessage("Test");
			await VerifyCSharpDiagnosticAsync(test, expected);
		}


		[Fact]
		public async Task VerifyConditionalDebugWorks2()
		{
			const string test = @"
using System.Diagnostics;

namespace BestPracticeChecker.Test
{
    class Something
    {
		[Conditional(""ENABLE_LOGS"")]
        void DoSomething(string message)
        {
            UnityEngine.Debug.Log(message); 
        }
    } 
}";

			await VerifyCSharpDiagnosticAsync(test);
		}
	}
}
