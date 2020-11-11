using Microsoft.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace BestPracticeChecker.Tests
{
    public class ObjectFindAndTypeAnalyzerTest : BaseDiagnosticVerifierTest<ObjectFindAndTypeAnalyzer>
    {     
        [Fact]
        public async Task GameObjectFindHighlightedInStart
            ()
        {
            const string test = @"
using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something : MonoBehaviour
    {   
        public GameObject prefab;
        public GameObject prefab2;

        Something()
        {
            prefab = new GameObject();
            prefab2 = new GameObject(""Prefab2"");
        }

        void Start()
        {
            prefab = GameObject.Find(""Prefab2"");
        }       
    } 
}";

            var expected = new DiagnosticResult("BP0022", DiagnosticSeverity.Warning)
                .WithLocation(19, 22)
                .WithMessage("Die Methoden GameObject.Find und GameObject.FindObjectOfType sollten generell nicht verwendet werden, da diese über alle verwendeten GameObjects iterieren und es damit schnell zu Performanceproblemen kommt.");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }

        [Fact]
        public async Task ObjectFindObjectOfTypeHighlightedInStart()
        {
            const string test = @"
using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something : MonoBehaviour
    {   
        
        void Start()
        {
            Camera cam = (Camera)Object.FindObjectOfType(typeof(Camera));
        }       
    } 
}";

            var expected = new DiagnosticResult("BP0022", DiagnosticSeverity.Warning)
                .WithLocation(11, 34)
                .WithMessage("Die Methoden GameObject.Find und GameObject.FindObjectOfType sollten generell nicht verwendet werden, da diese über alle verwendeten GameObjects iterieren und es damit schnell zu Performanceproblemen kommt.");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }

        [Fact]
        public async Task GameObjectFindHighlightedInAwake()
        {
            const string test = @"
using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something : MonoBehaviour
    {   
        public GameObject prefab;
        public GameObject prefab2;

        Something()
        {
            prefab = new GameObject();
            prefab2 = new GameObject(""Prefab2"");
        }

        void Awake()
        {
            prefab = GameObject.Find(""Prefab2"");
        }       
    } 
}";

            var expected = new DiagnosticResult("BP0022", DiagnosticSeverity.Warning)
                .WithLocation(19, 22)
                .WithMessage("Die Methoden GameObject.Find und GameObject.FindObjectOfType sollten generell nicht verwendet werden, da diese über alle verwendeten GameObjects iterieren und es damit schnell zu Performanceproblemen kommt.");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }

        [Fact]
        public async Task ObjectFindObjectOfTypeHighlightedInAwake()
        {
            const string test = @"
using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something : MonoBehaviour
    {   
        
        void Awake()
        {
            Camera cam = (Camera)Object.FindObjectOfType(typeof(Camera));
        }       
    } 
}";

            var expected = new DiagnosticResult("BP0022", DiagnosticSeverity.Warning)
                .WithLocation(11, 34)
                .WithMessage("Die Methoden GameObject.Find und GameObject.FindObjectOfType sollten generell nicht verwendet werden, da diese über alle verwendeten GameObjects iterieren und es damit schnell zu Performanceproblemen kommt.");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }

        //ToDo Check if call in any method  
        [Fact]
        public async Task ObjectFindObjectOfTypeHighlightedInAnyMethod()
        {
            const string test = @"
using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something : MonoBehaviour
    {   
        
        void DoSomething()
        {
            Camera cam = (Camera)Object.FindObjectOfType(typeof(Camera));
        }       
    } 
}";

            var expected = new DiagnosticResult("BP0022",DiagnosticSeverity.Warning)
                .WithLocation(11, 34)
                .WithMessage("Die Methoden GameObject.Find und GameObject.FindObjectOfType sollten generell nicht verwendet werden, da diese über alle verwendeten GameObjects iterieren und es damit schnell zu Performanceproblemen kommt.");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }

        [Fact]
        public async Task GameObjectFindHighlightedInAnyMethod
()
        {
            const string test = @"
using UnityEngine;

namespace BestPracticeChecker.Test
{
    class Something : MonoBehaviour
    {   
        public GameObject prefab;
        public GameObject prefab2;

        Something()
        {
            prefab = new GameObject();
            prefab2 = new GameObject(""Prefab2"");
        }

        void DoSomething()
        {
            prefab = GameObject.Find(""Prefab2"");
        }       
    } 
}";

            var expected = new DiagnosticResult("BP0022", DiagnosticSeverity.Warning)
                .WithLocation(19, 22)
                .WithMessage("Die Methoden GameObject.Find und GameObject.FindObjectOfType sollten generell nicht verwendet werden, da diese über alle verwendeten GameObjects iterieren und es damit schnell zu Performanceproblemen kommt.");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }
    }
}
