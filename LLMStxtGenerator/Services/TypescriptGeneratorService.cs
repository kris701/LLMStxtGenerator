using LLMStxtGenerator.Models.Config;
using System.Text;
using System.Text.RegularExpressions;

namespace LLMStxtGenerator.Services
{
	/// <summary>
	/// Generator for TS projects
	/// </summary>
	public class TypescriptGeneratorService : BaseGeneratorService<TypescriptGeneratorConfig>
	{
		private static Regex _declarationRegex = new Regex("\\b(?:export)?\\s*(?:class|interface|type|enum|const|let|var|function)\\s+[\\w$]+", RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);
		private static Regex _summaryRegex = new Regex("\\/\\*\\*(.*?)\\*\\/", RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);

		public override async Task<string> Generate(TypescriptGeneratorConfig config)
		{
			var allMatching = GetMatchingFiles(config);

			var sb = new StringBuilder();

			sb.AppendLine($"There are a total of {allMatching.Count} files relevant for you.");
			sb.AppendLine($"You should consider only these files, unless told otherwise.");
			foreach (var file in allMatching)
			{
				var fileText = File.ReadAllText(Path.Combine(config.WorkingDirectory, file));
				var indent = GetFileDepth(file);
				sb.Append($"{new string(' ', indent)} * \"{file}\"");

				if (config.IncludeDeclaration)
				{
					var declaration = GetDeclaration(fileText);
					if (declaration != null)
						sb.Append($". Declaration: \"{declaration}\"");
					else
						sb.Append(".");
				}
				if (config.IncludeSummary)
				{
					var summary = GetSummaryOrNull(fileText);
					if (summary != null)
						sb.Append($". Summary: \"{summary}\"");
					else
						sb.Append(".");
				}
				sb.AppendLine();
			}

			return sb.ToString();
		}

		private string? GetDeclaration(string fileText)
		{
			var matches = _declarationRegex.Matches(fileText);
			if (matches.Count == 0)
				return null;
			var value = "";
			foreach (Match match in matches)
			{
				if (match.Groups.Count > 0 && match.Groups[0].Value != "")
				{
					value = match.Groups[0].Value;
					break;
				}
			}

			value = value.Replace("\r", "");
			value = value.Replace("\n", "");
			value = value.Replace("\t", "");
			value = value.Trim();

			if (value == "")
				return null;

			return value;
		}

		private string? GetSummaryOrNull(string fileText)
		{
			var matches = _summaryRegex.Matches(fileText);
			if (matches.Count == 0)
				return null;
			var value = "";
			foreach (Match match in matches)
			{
				if (match.Groups.Count > 1 && match.Groups[1].Value != "")
				{
					value = match.Groups[1].Value;
					break;
				}
			}

			value = value.Replace("///", "");
			value = value.Replace("\r", "");
			value = value.Replace("\n", "");
			value = value.Replace("\t", "");
			value = value.Trim();

			if (value == "")
				return null;

			return value;
		}
	}
}
