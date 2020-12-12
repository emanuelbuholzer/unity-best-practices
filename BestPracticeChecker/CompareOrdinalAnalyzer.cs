﻿using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using BestPracticeChecker.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.Operations;

 namespace BestPracticeChecker
{
    using ArgumentExtractor = Func<SeparatedSyntaxList<ArgumentSyntax>, IEnumerable<ExpressionSyntax>>;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class CompareOrdinalAnalyzer : DiagnosticAnalyzer
    {

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
        private static readonly DiagnosticDescriptor Rule =
            new DiagnosticDescriptor(
                "BP0006",
                DiagnosticStrings.GetString(nameof(Strings.CompareStringWithOrdinalTitle)),
                DiagnosticStrings.GetString(nameof(Strings.CompareStringWithOrdinalMessageFormat)),
                DiagnosticStrings.DiagnosticCategory.Performance,
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                helpLinkUri: DiagnosticStrings.GetHelpLinkUri("BP0006_OrdinalStringComparison.md"));
        
        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.InvocationExpression);
            context.RegisterOperationAction(AnalyzeEqualOperation, OperationKind.BinaryOperator);
        }

        private void AnalyzeEqualOperation(OperationAnalysisContext context)
        {
            var operation = (IBinaryOperation)context.Operation;
            
            if (operation.OperatorKind != BinaryOperatorKind.Equals &&
                operation.OperatorKind != BinaryOperatorKind.NotEquals)
                return;

            if ("System".Equals(operation.LeftOperand.Type.ContainingNamespace.ToString()) &&
                "String".Equals(operation.LeftOperand.Type.Name) &&
                "System".Equals(operation.RightOperand.Type.ContainingNamespace.ToString()) &&
                "String".Equals(operation.RightOperand.Type.Name))
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, operation.Syntax.GetLocation())); 
            }
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var invocationExpression = (InvocationExpressionSyntax)context.Node;
            var methodSymbol = context.SemanticModel.GetSymbolInfo(invocationExpression, context.CancellationToken).Symbol as IMethodSymbol;

            var methods= new List<Symbol>()
            {
                Symbol.From("System", "String", "Equals"),
                Symbol.From("System", "String", "Compare"),
            };
            
            if (!methods.Any(m => m.Equals(methodSymbol)))
                return;

            var allowedComparisons = new List<string>()
            {
                "StringComparison.Ordinal",
                "StringComparison.OrdinalIgnoreCase"
            };

            var arguments = invocationExpression.ArgumentList.Arguments;
            if (allowedComparisons.Any(a => 
                arguments[arguments.Count-1].Expression.GetText().ToString().Equals(a)))
                return;
            
            context.ReportDiagnostic(Diagnostic.Create(Rule, invocationExpression.GetLocation()));
        }
    }
}
