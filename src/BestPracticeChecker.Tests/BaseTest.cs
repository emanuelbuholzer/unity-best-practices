using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Xunit;

namespace BestPracticeChecker.Tests
{
	public abstract class BaseDiagnosticVerifierTest<TAnalyzer> : DiagnosticVerifier
		where TAnalyzer : DiagnosticAnalyzer, new() 
	{

		internal const string InterfaceTest = @"
interface IFailure
{
	void FixedUpdate();
}
";

		[Fact]
		public async Task DoNotFailWithInterfaceMembers()
		{
			await VerifyCSharpDiagnosticAsync(InterfaceTest);
		}

		protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
		{
			return new TAnalyzer();
		}
	}
}
