using System.Threading.Tasks;
using BestPracticeChecker;
using BestPracticeChecker.Resources;
using Microsoft.CodeAnalysis;
using Xunit;

namespace BestPracticeChecker.Tests
{
	public class ConstTagsAnalyzerTest : BaseDiagnosticVerifierTest<ConstTagsAnalyzer>
	{

		[Fact]
		public async Task VariableTag()
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

			var expected = new DiagnosticResult("BP0004", DiagnosticSeverity.Warning)
				.WithLocation(11, 41)
				.WithMessage(DiagnosticStrings.GetString("ConstTagsMessageFormat").ToString());
			await VerifyCSharpDiagnosticAsync(test, expected);
		}
		
		[Fact]
		public async Task ArgumentTag()
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

			var expected = new DiagnosticResult("BP0004", DiagnosticSeverity.Warning)
				.WithLocation(10, 41)
				.WithMessage(DiagnosticStrings.GetString("ConstTagsMessageFormat").ToString());
			await VerifyCSharpDiagnosticAsync(test, expected);
		}

		

		[Fact]
		public async Task InlineConstantTag()
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
		public async Task ConstantTag()
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
		public async Task ClassMemberConstantTag()
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
	}
}
