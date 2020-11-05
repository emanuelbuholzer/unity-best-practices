using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace BestPracticeChecker.Tests
{
    public class ConditionalDebugAnalyzerTest : BaseDiagnosticVerifierTest<ConditionalDebugAnalyzer>
    {

        [Fact]
        public async Task VerifyConditionalDebugLog()
        {
            var methods = new List<String>()
            {
                "Debug.Log(message);",
                "Debug.LogAssertion(message);",
                "Debug.LogError(message);",
                "Debug.LogWarning(message);",
                "Debug.LogAssertionFormat(message);",
                "Debug.LogErrorFormat(message);",
                "Debug.LogWarningFormat(message);"
            };

            var tests = methods.Select(m => $@"
using UnityEngine;

namespace BestPracticeChecker.Test
{{
    class Something
    {{
        void DoSomething(string message)
        {{
            {m}
        }}
    }} 
}}
");
            
            var expected = new DiagnosticResult("ConditionalDebug", DiagnosticSeverity.Warning)
                .WithLocation(10, 13)
                .WithMessage("Use UnityEngine.Debug Statements only with a Conditional Attribute.");
            
            foreach (var test in tests)
            {
                await VerifyCSharpDiagnosticAsync(test, expected); 
            }
        }

        [Fact]
        public async Task VerifyConditionalDebugLog02()
        {
            var methods = new List<String>()
            {
                "UnityEngine.Debug.Log(message);",
                "UnityEngine.Debug.LogAssertion(message);",
                "UnityEngine.Debug.LogError(message);",
                "UnityEngine.Debug.LogWarning(message);",
                "UnityEngine.Debug.LogAssertionFormat(message);",
                "UnityEngine.Debug.LogErrorFormat(message);",
                "UnityEngine.Debug.LogWarningFormat(message);"
            };

            var tests = methods.Select(m => $@"
using System.Diagnostics;

namespace BestPracticeChecker.Test
{{
    class Something
    {{  
        [Conditional(""ENABLE_LOG_EXCEPTION"")]
        void DoSomething(string message)
        {{
            {m}
        }}
    }} 
}}
");
            foreach (var test in tests)
            {
                await VerifyCSharpDiagnosticAsync(test);
            }
        }



        [Fact]
        public async Task VerifyConditionalDebugWorksLogException()
        {
            const string test = @"
using UnityEngine;
using System;

namespace BestPracticeChecker.Test
{
    class Something
    {       
        Exception b  = new Exception();
        
        void DoSomething()
        {
            Debug.LogException(b);
        }
    }
    
}";

            var expected = new DiagnosticResult("ConditionalDebug", DiagnosticSeverity.Warning)
                .WithLocation(13, 13)
                .WithMessage("Use UnityEngine.Debug Statements only with a Conditional Attribute.");
            await VerifyCSharpDiagnosticAsync(test, expected);
        }


        [Fact]
        public async Task VerifyConditionalDebugWorksLogExecption01()
        {
            const string test = @"
using System.Diagnostics;
using System;

namespace BestPracticeChecker.Test
{
    class Something
    {   
        Exception b  = new Exception();
        
        [Conditional(""ENABLE_LOG_EXCEPTION"")]
        void DoSomething()
        {
            UnityEngine.Debug.LogException(b);
        }
    } 
}";

            await VerifyCSharpDiagnosticAsync(test);
        }


        [Fact]
        public async Task VerifyConditionalDebugLogStruct()
        {

            const string test = @"
using UnityEngine;

namespace BestPracticeChecker.Test
{
    struct Something
    {
        string message {get; set;}

        void  DoSomething(string message)
        {
             this.message = message;
             Debug.Log(message);
        }
    }
}";
            var expected = new DiagnosticResult("ConditionalDebug", DiagnosticSeverity.Warning)
                .WithLocation(13, 14)
                .WithMessage("Use UnityEngine.Debug Statements only with a Conditional Attribute.");
            await VerifyCSharpDiagnosticAsync(test, expected);

        }


        [Fact]
        public async Task VerifyConditionalDebugWorksLogStruct02()
        {
            const string test = @"
using System.Diagnostics;

namespace BestPracticeChecker.Test
{
    struct Something
    {
        string message {get; set;}

        [Conditional(""ENABLE_LOG_WARNING"")]
        void  DoSomething(string message)
        {
            this.message = message;
            UnityEngine.Debug.Log(message);
        }
    }
}";
            await VerifyCSharpDiagnosticAsync(test);
        }

    }
}



