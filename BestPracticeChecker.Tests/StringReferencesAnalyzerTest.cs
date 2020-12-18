using Microsoft.CodeAnalysis;
using System.Threading.Tasks;
using BestPracticeChecker.Resources;
using Xunit;

namespace BestPracticeChecker.Tests
{
    public class StringReferencesAnalyzerTest : DiagnosticVerifier<StringReferencesAnalyzer>
    {

        [Fact]
        public async Task StringReferenceShouldReport()
        {
            const string test = @"
using System;
using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something : MonoBehaviour
    {
        void DoSomething(string a)
        {
            StartCoroutine(""DoSomething"", a);
        }
    } 
}";

            var expected = new DiagnosticResult("BP0003", DiagnosticSeverity.Warning)
                .WithLocation(11, 13)
                .WithMessage(DiagnosticStrings.GetString("StringReferencesMessageFormat").ToString());
            await VerifyCSharpDiagnosticAsync(test, expected);
        }


        [Fact]
        public async Task StringStartsWithHighlighted()
        {
            const string test = @"
using System;
using UnityEngine;
using System.Collections;

namespace BestPracticeChecker.Test
{
    class Something : MonoBehaviour
    {
        private IEnumerator WaitAndPrint(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
            }
        }        

        void DoSomething(string a)
        {
            var coroutine = WaitAndPrint(2.0f);
            StartCoroutine(coroutine);
        }
    } 
}";
            await VerifyCSharpDiagnosticAsync(test);
        }
    }
}
