using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using BestPracticeChecker.Resources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace BestPracticeChecker
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class IllegalMethodInUpdateAnalyzer : SyntaxNodeInMethodAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
        private static readonly DiagnosticDescriptor Rule = 
            new DiagnosticDescriptor(
                "BP0001", 
                DiagnosticStrings.GetString(nameof(Strings.IllegalMethodInUpdateTitle)), 
                DiagnosticStrings.GetString(nameof(Strings.IllegalMethodInUpdateMessageFormat)), 
                DiagnosticStrings.DiagnosticCategory.Performance, 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true, 
                description: DiagnosticStrings.GetString(nameof(Strings.IllegalMethodInUpdateDescription)),
                helpLinkUri: DiagnosticStrings.GetHelpLinkUri("BP0001_MethodsToAvoidInUpdate.md"));

        protected override SyntaxKind ByKind => SyntaxKind.InvocationExpression;

        protected override ImmutableList<Symbol> InMethods =>
            ImmutableList.Create(Symbol.From("UnityEngine", "MonoBehaviour", "Update"));
        protected override ImmutableList<Symbol> FilterBySymbols => 
            ImmutableList.Create(
                Symbol.From("UnityEngine", "GameObject", "GetComponent"),
                Symbol.From("UnityEngine", "GameObject", "GetComponents"),
                Symbol.From("UnityEngine", "GameObject", "GetComponentInParent"),
                Symbol.From("UnityEngine", "GameObject", "GetComponentsInParent"),
                Symbol.From("UnityEngine", "GameObject", "FindObjectOfType"),
                Symbol.From("UnityEngine", "GameObject", "FindObjectsOfType"),
                Symbol.From("UnityEngine", "Component", "GetComponents"),
                Symbol.From("UnityEngine", "Component", "GetComponent"),
                Symbol.From("UnityEngine", "Component", "GetComponentInParent"),
                Symbol.From("UnityEngine", "Component", "GetComponentsInParent"),
                Symbol.From("UnityEngine", "Component", "FindObjectOfType"),
                Symbol.From("UnityEngine", "Component", "FindObjectsOfType"),
                Symbol.From("UnityEngine", "Object", "FindObjectOfType"),
                Symbol.From("UnityEngine", "Object", "FindObjectsOfType")
                );

        public override void AnalyzeNodeInMethod(SemanticModelAnalysisContext context, SyntaxNode node)
        {
            context.ReportDiagnostic(Diagnostic.Create(Rule, node.GetLocation()));
        }
    }
}