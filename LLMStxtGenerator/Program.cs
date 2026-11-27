using CommandLine;
using CommandLine.Text;
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
		var parserResult = parser.ParseArguments<Options>(args);
		parserResult.WithNotParsed(errs => DisplayHelp(parserResult, errs));
		await parserResult.WithParsedAsync(Run);
	}

	private static async Task Run(Options opts)
	{
		Console.WriteLine("Reading config file...");
		var text = await File.ReadAllTextAsync(opts.ConfigFile);
		var parsed = JsonSerializer.Deserialize<IGeneratorConfig>(text, new JsonSerializerOptions()
		{
			AllowTrailingCommas = true,
			ReadCommentHandling = JsonCommentHandling.Skip,
		});
		if (parsed == null)
			throw new Exception("Could not parse config file!");

		Console.WriteLine("Starting generator...");
		var result = await GenerateLLMsTxt(parsed);

		Console.WriteLine("Writing file...");
		await File.WriteAllTextAsync(opts.OutputPath, result);

		Console.WriteLine("Done!");
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
				Console.WriteLine(sentenceBuilder.FormatError(error));
	}

	private static void DisplayHelp<T>(ParserResult<T> result, IEnumerable<CommandLine.Error> errs)
	{
		var helpText = HelpText.AutoBuild(result, h =>
		{
			h.AddEnumValuesToHelpText = true;
			return h;
		}, e => e, verbsIndex: true);
		Console.WriteLine(helpText);
		HandleParseError(errs);
	}
}