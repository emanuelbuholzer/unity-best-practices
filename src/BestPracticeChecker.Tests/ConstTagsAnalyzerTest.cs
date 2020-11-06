using System.Threading.Tasks;
using BestPracticeChecker;
using Microsoft.CodeAnalysis;
using Xunit;

namespace BestPracticeChecker.Tests
{
	public class ConstTagsAnalyzerTest : BaseDiagnosticVerifierTest<ConstTagsAnalyzer>
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
        void Start()
        {
			string a = ""Respawn"";
			var respawn = GameObject.FindWithTag(a);
        }
    } 
}";

			var expected = new DiagnosticResult("ConstTags", DiagnosticSeverity.Warning)
				.WithLocation(11, 41)
				.WithMessage("Tags sollten nur mit Konstanten referenziert werden.");
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
        void Start(string a)
        {
			var respawn = GameObject.FindWithTag(a);
        }
    } 
}";

			var expected = new DiagnosticResult("ConstTags", DiagnosticSeverity.Warning)
				.WithLocation(10, 41)
				.WithMessage("Tags sollten nur mit Konstanten referenziert werden.");
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
        void Start()
        {
			var respawn = GameObject.FindWithTag(""a"");
        }
    } 
}";

			await VerifyCSharpDiagnosticAsync(test);
		}
		
		[Fact]
		public async Task MonoBehaviourSingularNotInUpdateShouldNotThrow2()
		{
			const string test = @"
using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something : MonoBehaviour
    {
        void Start()
        {
			const string a = ""a"";
			var respawn = GameObject.FindWithTag(a);
        }
    } 
}";

			await VerifyCSharpDiagnosticAsync(test);
		}
		
		[Fact]
		public async Task MonoBehaviourSingularNotInUpdateShouldNotThrow3()
		{
			const string test = @"
using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something : MonoBehaviour
    {
        public const string a = ""a"";

		void B()
		{
			var respawn = GameObject.FindWithTag(a);
		}
    } 
}";

			await VerifyCSharpDiagnosticAsync(test);
		}
		
		
		[Fact]
		public async Task MonoBehaviourSingularNotInUpdateShouldNotThrow4()
		{
			const string test = @"
using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something : MonoBehaviour
    {
		void B()
		{
			Application.SetBuildTags(new string[] { ""a""});
		}
    } 
}";
			// TODO: Currently not working
			// await VerifyCSharpDiagnosticAsync(test);
		}
	}
}
