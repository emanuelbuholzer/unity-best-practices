using System.Threading.Tasks;
using BestPracticeChecker;
using BestPracticeChecker.Resources;
using Microsoft.CodeAnalysis;
using Xunit;

namespace BestPracticeChecker.Tests
{
	public class IllegalMethodInUpdateAnalyzerTest : BaseDiagnosticVerifierTest<IllegalMethodInUpdateAnalyzer>
	{

		[Fact]
		public async Task MonoBehaviourSingularInUpdateShouldThrow()
		{
			const string test = @"
using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something : MonoBehaviour
    {
        void Update()
        {
			HingeJoint hinge = gameObject.GetComponent(typeof(HingeJoint)) as HingeJoint;
        }
    } 
}";

			var expected = new DiagnosticResult("BP0001", DiagnosticSeverity.Warning)
				.WithLocation(10, 23)
				.WithMessage(DiagnosticStrings.GetString("IllegalMethodInUpdateMessageFormat").ToString());
			await VerifyCSharpDiagnosticAsync(test, expected);
		}
		
		[Fact]
		public async Task MonoBehaviourPluralInUpdateShouldThrow()
		{
			const string test = @"
using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something : MonoBehaviour
    {
        void Update()
        {
			DoSomething();
        }

		void DoSomething()
		{
			var hinges = gameObject.GetComponents(typeof(HingeJoint)) as HingeJoint[];
		}
    } 
}";

			var expected = new DiagnosticResult("BP0001", DiagnosticSeverity.Warning)
				.WithLocation(10, 17)
				.WithMessage(DiagnosticStrings.GetString("IllegalMethodInUpdateMessageFormat").ToString());
			//await VerifyCSharpDiagnosticAsync(test, expected);
		}

		

		[Fact]
		public async Task MonoBehaviourSingularNotInUpdateShouldNotThrow()
		{
			const string test = @"
using UnityEngine; 

namespace BestPracticeChecker.Test
{
    class Something : MonoBehaviour
    {
        void DoSomething(string message)
        {
			HingeJoint hinge = gameObject.GetComponent(typeof(HingeJoint)) as HingeJoint; 
        }
    } 
}";

			//await VerifyCSharpDiagnosticAsync(test);
		}
	}
}
