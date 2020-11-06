using System.Threading.Tasks;
using BestPracticeChecker;
using BestPracticeChecker.Resources;
using Microsoft.CodeAnalysis;
using Xunit;

namespace BestPracticeChecker.Tests
{
	public class CameraMainInUpdateAnalyzerTest : BaseDiagnosticVerifierTest<CamaraMainInUpdateAnalyzer>
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
		Camera m_MainCamera;
        void Update()
        {
			m_MainCamera = Camera.main;
        }
    } 
}";
			
			var expected = new DiagnosticResult("BP0023", DiagnosticSeverity.Warning)
				.WithLocation(11, 19)
				.WithMessage(DiagnosticStrings.GetString("ConstTagsMessageFormat").ToString());
			await VerifyCSharpDiagnosticAsync(test, expected);
		}
		
	}
}
