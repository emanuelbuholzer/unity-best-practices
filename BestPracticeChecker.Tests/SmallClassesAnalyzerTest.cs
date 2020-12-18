using Microsoft.CodeAnalysis;
using System.Threading.Tasks;
using BestPracticeChecker.Resources;
 using Xunit;

namespace BestPracticeChecker.Tests
{
    public class SmallClassesAnalyzerTest : DiagnosticVerifier<SmallClassesAnalyzer>
    {
        [Fact]
        public async Task BigClassShouldReport()
        {
            const string test = @"

using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomeCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results,float distance, int layerMask, float maxDepth, float minDepth) 
        { 
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
        }
    } 
}";
            var expected = new DiagnosticResult("BP0008", DiagnosticSeverity.Warning)
                .WithLocation(7, 5)
                .WithMessage(DiagnosticStrings.GetString("SmallClassesMessageFormat").ToString());
            await VerifyCSharpDiagnosticAsync(test, expected);
        }
        
        [Fact]
        public async Task SmallClassShouldNotReport()
        {
            const string test = @"

using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomeCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results,float distance, int layerMask, float maxDepth, float minDepth) 
        { 
            Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, maxDepth, minDepth);
        }
    } 
}";
            await VerifyCSharpDiagnosticAsync(test);
        }
    }
}
