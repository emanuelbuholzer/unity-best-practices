using Microsoft.CodeAnalysis;

namespace BestPracticeChecker.Tests
{
	public readonly struct DiagnosticLocation
	{
		public DiagnosticLocation(FileLinePositionSpan span, DiagnosticLocationOptions options)
		{
			Span = span;
			Options = options;
		}

		public FileLinePositionSpan Span { get; }

		public DiagnosticLocationOptions Options { get; }
	}
}
