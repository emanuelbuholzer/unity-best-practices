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
		public Task CacheInStartSShouldBeOk()
		{
			const string test = @"
using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something : MonoBehaviour
    {
		Camera _MainCamera;

		void Start()
		{
		    _MainCamera = Camera.main;
		    // Do something
		}

		void Update() 
		{
		    _MainCamera.enabled = false;
		}
    } 
}";
			
			return VerifyCSharpDiagnosticAsync(test);
		}

		[Fact]
		public Task AccesssInUpdateShouldReport()
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
			return VerifyCSharpDiagnosticAsync(test, expected);
		}
		
		[Fact]
		public Task AccessViaMethodInUpdateShouldReport()
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
			DoSomething();
        }

		void DoSomething() 
		{
			m_MainCamera = Camera.main;	
		}
    } 
}";
			
			var expected = new DiagnosticResult("BP0023", DiagnosticSeverity.Warning)
				.WithLocation(16, 19)
				.WithMessage(DiagnosticStrings.GetString("ConstTagsMessageFormat").ToString());
			return VerifyCSharpDiagnosticAsync(test, expected);
		}
		
		[Fact]
		public Task AccessViaClassMethodInUpdateShouldReport()
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
			m_MainCamera = Something2.GetCamera();
        }
    } 

	static class Something2
	{
		public static Camera GetCamera()
		{
			return Camera.main;
		}
	}
}";
			
			var expected = new DiagnosticResult("BP0023", DiagnosticSeverity.Warning)
				.WithLocation(19, 11)
				.WithMessage(DiagnosticStrings.GetString("ConstTagsMessageFormat").ToString());
			return VerifyCSharpDiagnosticAsync(test, expected);
		}
		
		[Fact]
		public Task AccessViaMethodUsingClassMethodInUpdateShouldReport()
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
			m_MainCamera = Somethingy();
        }

		Camera Somethingy()
		{
			return Something2.GetCamera();
		}
    } 

	static class Something2
	{
		public static Camera GetCamera()
		{
			return Camera.main;
		}
	}
}";
			
			var expected = new DiagnosticResult("BP0023", DiagnosticSeverity.Warning)
				.WithLocation(24, 11)
				.WithMessage(DiagnosticStrings.GetString("ConstTagsMessageFormat").ToString());
			return VerifyCSharpDiagnosticAsync(test, expected);
		}
	}
}
