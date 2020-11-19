﻿using Microsoft.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace BestPracticeChecker.Tests
{
    public class AllocApiTest : BaseDiagnosticVerifierTest<AllocApi>
    {
        //Test for all Physics

        [Fact]
        public async Task AllocApiBoxcastHighlighted()
        {
            const string test = @"
using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomeCast(Vector3 pos, Vector3 n, Vector3 dir) 
        { 
            Physics.BoxCast(pos, n, dir, Quaternion.LookRotation(dir));
        }
    } 
}";

            var expected = new DiagnosticResult("BP0020", DiagnosticSeverity.Warning)
                .WithLocation(10, 29)
                .WithMessage("Verwende nicht allozierende APIs ab Unity 5.3 oder neuer durch den Suffix NonAlloc, da diese nicht unnötig viel Speicher aollozieren.");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }


        [Fact]
        public async Task AllocApiCapsuleCastHighlighted()
        {
            const string test = @"

using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomeCast(Vector3 point1,Vector3 point2,float r, Vector3 dir, float castDistance) 
        { 
            Physics.CapsuleCast(point1, point2, r, dir, castDistance);
        }
    } 
}";

            var expected = new DiagnosticResult("BP0020", DiagnosticSeverity.Warning)
                .WithLocation(11, 33)
                .WithMessage("Verwende nicht allozierende APIs ab Unity 5.3 oder neuer durch den Suffix NonAlloc, da diese nicht unnötig viel Speicher aollozieren.");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }

        [Fact]
        public async Task AllocApiOverlapBoxHighlighted()
        {
            const string test = @"

using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomeCast(Vector3 center,Vector3 halfExtends,Quaternion orientation,int layerMask, QueryTriggerInteraction queryTriggerInteraction) 
        { 
            Physics.OverlapBox(center, halfExtends, orientation, layerMask, queryTriggerInteraction);
        }
    } 
}";

            var expected = new DiagnosticResult("BP0020", DiagnosticSeverity.Warning)
                .WithLocation(11, 32)
                .WithMessage("Verwende nicht allozierende APIs ab Unity 5.3 oder neuer durch den Suffix NonAlloc, da diese nicht unnötig viel Speicher aollozieren.");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }

        [Fact]
        public async Task AllocApiRaycastHighlighted()
        {
            const string test = @"

using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomeCast(Vector3 point1,Vector3 point2,float r, int layerMask, QueryTriggerInteraction queryTriggerInteraction) 
        { 
            Physics.OverlapCapsule(point1, point2, r, layerMask, queryTriggerInteraction);
        }
    } 
}";

            var expected = new DiagnosticResult("BP0020", DiagnosticSeverity.Warning)
                .WithLocation(11, 36)
                .WithMessage("Verwende nicht allozierende APIs ab Unity 5.3 oder neuer durch den Suffix NonAlloc, da diese nicht unnötig viel Speicher aollozieren.");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }

        [Fact]
        public async Task AllocApiOverlapSphereHighlighted()
        {
            const string test = @"

using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomeCast(Vector3 pos, float r, int layerMask, QueryTriggerInteraction queryTriggerInteraction) 
        { 
            Physics.OverlapSphere(pos, r, layerMask, queryTriggerInteraction);
        }
    } 
}";

            var expected = new DiagnosticResult("BP0020", DiagnosticSeverity.Warning)
                .WithLocation(11, 35)
                .WithMessage("Verwende nicht allozierende APIs ab Unity 5.3 oder neuer durch den Suffix NonAlloc, da diese nicht unnötig viel Speicher aollozieren.");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }

        [Fact]
        public async Task AllocRaycastHighlighted()
        {
            const string test = @"

using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomeCast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction) 
        { 
            Physics.Raycast(origin, direction, maxDistance, layerMask, queryTriggerInteraction);
        }
    } 
}";

            var expected = new DiagnosticResult("BP0020", DiagnosticSeverity.Warning)
                .WithLocation(11, 29)
                .WithMessage("Verwende nicht allozierende APIs ab Unity 5.3 oder neuer durch den Suffix NonAlloc, da diese nicht unnötig viel Speicher aollozieren.");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }


        [Fact]
        public async Task AllocSphereCast()
        {
            const string test = @"

using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomeCast(Vector3 origin,float r,Vector3 direction, out RaycastHit hitInfo,float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction) 
        { 
            Physics.SphereCast(origin, r, direction,out hitInfo,  maxDistance, layerMask, queryTriggerInteraction);
        }
    } 
}";

            var expected = new DiagnosticResult("BP0020", DiagnosticSeverity.Warning)
                .WithLocation(11, 32)
                .WithMessage("Verwende nicht allozierende APIs ab Unity 5.3 oder neuer durch den Suffix NonAlloc, da diese nicht unnötig viel Speicher aollozieren.");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }


        //Tests for only 2 Methods from Physics2D

        [Fact]
        public async Task AllocLinecastHighlighted()
        {
            const string test = @"

using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomeCast(Vector2 start, Vector2 end, int layerMask, float minDepth, float maxDepth) 
        { 
            Physics2D.Linecast(start, end, layerMask, minDepth, maxDepth);
        }
    } 
}";

            var expected = new DiagnosticResult("BP0020", DiagnosticSeverity.Warning)
                .WithLocation(11, 32)
                .WithMessage("Verwende nicht allozierende APIs ab Unity 5.3 oder neuer durch den Suffix NonAlloc, da diese nicht unnötig viel Speicher aollozieren.");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }

        [Fact]
        public async Task AllocRaycast2DHighlighted()
        {
            const string test = @"

using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomeCast(Vector2 origin, Vector2 direction,float distance, int layerMask, float minDepth, float maxDepth) 
        { 
            Physics2D.Raycast(origin, direction, distance, layerMask, minDepth, maxDepth);
        }
    } 
}";

            var expected = new DiagnosticResult("BP0020", DiagnosticSeverity.Warning)
                .WithLocation(11, 31)
                .WithMessage("Verwende nicht allozierende APIs ab Unity 5.3 oder neuer durch den Suffix NonAlloc, da diese nicht unnötig viel Speicher aollozieren.");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }

        //Tests for NonAlloc-Methods ==> only 2 Tests (one for .Physics and one for .Physics2D)

        [Fact]
        public async Task NonAllocBoxcastNoHighlighted()
        {
            const string test = @"

using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something
    {
        void DoSomeCast(Vector3 center, Vector3 halfExtends, Vector3 direction, RaycastHit[] results,Quaternion orientation, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction) 
        { 
            Physics.BoxCastNonAlloc(center, halfExtends, direction, results, orientation, maxDistance, layerMask, queryTriggerInteraction);
        }
    } 
}";
            await VerifyCSharpDiagnosticAsync(test);
        }

        [Fact]
        public async Task NonAllocBoxcast2DNoHighlighted()
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
