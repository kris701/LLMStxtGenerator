using CommandLine;
using CommandLine.Text;
using LLMStxtGenerator.Helpers;
using LLMStxtGenerator.Models;
using LLMStxtGenerator.Models.Config;
using LLMStxtGenerator.Services;
using System.Text.Json;

/// <summary>
/// Entry CLI program
/// </summary>
internal class Program
{
	public static async Task Main(string[] args)
	{
		var parser = new Parser(with => with.HelpWriter = null);
		var parserResult = parser.ParseArguments<OptionsSingle, OptionsMultiple>(args);
		parserResult.WithNotParsed(errs => DisplayHelp(parserResult, errs));
		await parserResult.WithParsedAsync<OptionsSingle>(RunSingle);
		await parserResult.WithParsedAsync<OptionsMultiple>(RunMultiple);
	}

	private static async Task RunSingle(OptionsSingle opts)
	{
		ConsoleHelpers.WriteColor("Reading config file...   ", ConsoleColor.Blue);
		var text = await File.ReadAllTextAsync(opts.ConfigFile);
		var parsed = JsonSerializer.Deserialize<IGeneratorConfig>(text, new JsonSerializerOptions()
		{
			AllowTrailingCommas = true,
			ReadCommentHandling = JsonCommentHandling.Skip,
		});
		if (parsed == null)
			throw new Exception("Could not parse config file!");
		ConsoleHelpers.WriteLineColor("Done!", ConsoleColor.Green);

		ConsoleHelpers.WriteColor("Starting generator...   ", ConsoleColor.Blue);
		var result = await GenerateLLMsTxt(parsed);
		ConsoleHelpers.WriteLineColor("Done!", ConsoleColor.Green);

		ConsoleHelpers.WriteColor("Writing file...   ", ConsoleColor.Blue);
		await File.WriteAllTextAsync(parsed.OutputDirectory, result);
		ConsoleHelpers.WriteLineColor("Done!", ConsoleColor.Green);
	}

	private static async Task RunMultiple(OptionsMultiple opts)
	{
		ConsoleHelpers.WriteLineColor($"A total of {opts.ConfigFiles.Count()} config files to process.", ConsoleColor.Blue);

		foreach (var config in opts.ConfigFiles)
		{
			ConsoleHelpers.WriteColor("Reading config file...", ConsoleColor.Blue);
			var text = await File.ReadAllTextAsync(config);
			var parsed = JsonSerializer.Deserialize<IGeneratorConfig>(text, new JsonSerializerOptions()
			{
				AllowTrailingCommas = true,
				ReadCommentHandling = JsonCommentHandling.Skip,
			});
			if (parsed == null)
				throw new Exception("Could not parse config file!");
			ConsoleHelpers.WriteLineColor("Done!", ConsoleColor.Green);

			ConsoleHelpers.WriteColor("Starting generator...", ConsoleColor.Blue);
			var result = await GenerateLLMsTxt(parsed);
			ConsoleHelpers.WriteLineColor("Done!", ConsoleColor.Green);

			ConsoleHelpers.WriteColor("Writing file...", ConsoleColor.Blue);
			await File.WriteAllTextAsync(parsed.OutputDirectory, result);
			ConsoleHelpers.WriteLineColor("Done!", ConsoleColor.Green);
		}
	}

	private static async Task<string> GenerateLLMsTxt(IGeneratorConfig config)
	{
		switch (config)
		{
			case CSharpGeneratorConfig c:
				var cSharpGenerator = new CSharpGeneratorService();
				return await cSharpGenerator.Generate(c);
			case TypescriptGeneratorConfig c:
				var tsGenerator = new TypescriptGeneratorService();
				return await tsGenerator.Generate(c);
			default:
				throw new Exception("Unknown config type!");
		}
	}

	private static void HandleParseError(IEnumerable<CommandLine.Error> errs)
	{
		var sentenceBuilder = SentenceBuilder.Create();
		foreach (var error in errs)
			if (error is not HelpRequestedError)
				ConsoleHelpers.WriteLineColor(sentenceBuilder.FormatError(error), ConsoleColor.Red);
	}

	private static void DisplayHelp<T>(ParserResult<T> result, IEnumerable<CommandLine.Error> errs)
	{
		var helpText = HelpText.AutoBuild(result, h =>
		{
			h.AddEnumValuesToHelpText = true;
			return h;
		}, e => e, verbsIndex: true);
		ConsoleHelpers.WriteLineColor(helpText);
		HandleParseError(errs);
	}
}