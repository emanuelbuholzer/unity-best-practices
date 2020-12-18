using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace BestPracticeChecker.Tests
{
	public readonly struct DiagnosticResult
	{
		private static readonly object[] EmptyArguments = new object[0];

		private readonly ImmutableArray<DiagnosticLocation> _spans;
		private readonly bool _suppressMessage;
		private readonly string _message;

		public DiagnosticResult(string id, DiagnosticSeverity severity)
			: this()
		{
			Id = id;
			Severity = severity;
		}

		private DiagnosticResult(
			ImmutableArray<DiagnosticLocation> spans,
			bool suppressMessage,
			string message,
			DiagnosticSeverity severity,
			string id,
			LocalizableString messageFormat,
			object[] messageArguments,
			string suppressedId)
		{
			_spans = spans;
			_suppressMessage = suppressMessage;
			_message = message;
			Severity = severity;
			Id = id;
			MessageFormat = messageFormat;
			MessageArguments = messageArguments;
			SuppressedId = suppressedId;
		}

		public ImmutableArray<DiagnosticLocation> Spans => _spans.IsDefault ? ImmutableArray<DiagnosticLocation>.Empty : _spans;

		public DiagnosticSeverity Severity { get; }

		public string Id { get; }
		public string SuppressedId { get; }

		public string Message
		{
			get
			{
				if (_suppressMessage) return null;

				if (_message != null) return _message;

				if (MessageFormat != null) return string.Format(MessageFormat.ToString(), MessageArguments ?? EmptyArguments);

				return null;
			}
		}

		public LocalizableString MessageFormat { get; }

		public object[] MessageArguments { get; }

		public bool HasLocation => !Spans.IsEmpty;

		public DiagnosticResult WithMessage(string message)
		{
			return new DiagnosticResult(
				_spans,
				message is null,
				message,
				Severity,
				Id,
				MessageFormat,
				MessageArguments,
				SuppressedId);
		}

		public DiagnosticResult WithLocation(int line, int column)
			=> WithLocation(string.Empty, new LinePosition(line - 1, column - 1));

		public DiagnosticResult WithLocation(string path, LinePosition location)
			=> AppendSpan(new FileLinePositionSpan(path, location, location), DiagnosticLocationOptions.IgnoreLength);

		public DiagnosticResult WithSpan(string path, int startLine, int startColumn, int endLine, int endColumn)
			=> AppendSpan(new FileLinePositionSpan(path, new LinePosition(startLine - 1, startColumn - 1), new LinePosition(endLine - 1, endColumn - 1)), DiagnosticLocationOptions.None);

		private DiagnosticResult AppendSpan(FileLinePositionSpan span, DiagnosticLocationOptions options)
		{
			return new DiagnosticResult(
				Spans.Add(new DiagnosticLocation(span, options)),
				_suppressMessage,
				_message,
				Severity,
				Id,
				MessageFormat,
				MessageArguments,
				SuppressedId);
		}

		public override string ToString()
		{
			var builder = new StringBuilder();
			if (HasLocation)
			{
				var location = Spans[0];
				builder.Append(location.Span.Path == string.Empty ? "?" : location.Span.Path);
				builder.Append("(");
				builder.Append(location.Span.StartLinePosition.Line + 1);
				builder.Append(",");
				builder.Append(location.Span.StartLinePosition.Character + 1);
				if (!location.Options.HasFlag(DiagnosticLocationOptions.IgnoreLength))
				{
					builder.Append(",");
					builder.Append(location.Span.EndLinePosition.Line + 1);
					builder.Append(",");
					builder.Append(location.Span.EndLinePosition.Character + 1);
				}

				builder.Append("): ");
			}

			builder.Append(Severity.ToString().ToLowerInvariant());
			builder.Append(" ");
			builder.Append(Id);

			try
			{
				var message = Message;
				if (message != null) builder.Append(": ").Append(message);
			}
			catch (FormatException)
			{
				// A message format is provided without arguments, so we print the unformatted string
				Debug.Assert(MessageFormat != null, $"Assertion failed: {nameof(MessageFormat)} != null");
				builder.Append(": ").Append(MessageFormat);
			}

			return builder.ToString();
		}
	}
}
