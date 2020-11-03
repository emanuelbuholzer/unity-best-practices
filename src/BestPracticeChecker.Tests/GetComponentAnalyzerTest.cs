using System.Threading.Tasks;
using BestPracticeChecker;
using Microsoft.CodeAnalysis;
using Xunit;

namespace BestPracticeChecker.Tests
{
	public class GetComponentAnalyzerTest : BaseDiagnosticVerifierTest<GetComponentAnalyzer>
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

			var expected = new DiagnosticResult("GetComponent", DiagnosticSeverity.Warning)
				.WithLocation(10, 23)
				.WithMessage("GetComponent() und GetComponents() sollte aus Performanzgründen nie in der Update() Methode aufgerufen werden.");
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
			var hinges = gameObject.GetComponents(typeof(HingeJoint)) as HingeJoint[];
        }
    } 
}";

			var expected = new DiagnosticResult("GetComponent", DiagnosticSeverity.Warning)
				.WithLocation(10, 17)
				.WithMessage("GetComponent() und GetComponents() sollte aus Performanzgründen nie in der Update() Methode aufgerufen werden.");
			await VerifyCSharpDiagnosticAsync(test, expected);
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

			await VerifyCSharpDiagnosticAsync(test);
		}
	}
}
