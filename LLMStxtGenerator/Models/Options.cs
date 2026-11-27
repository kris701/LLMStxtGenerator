using CommandLine;

namespace LLMStxtGenerator.Models
{
	/// <summary>
	/// The model used for CLI input
	/// </summary>
	public class Options
	{
		[Option('c', "config", Required = true, HelpText = "A JSON file with configurations for how to generate the llms.txt file")]
		public string ConfigFile { get; set; }

		[Option('o', "output", Required = false, HelpText = "Path to where to put the result", Default = "llms.txt")]
		public string OutputPath { get; set; } = "llms.txt";
	}
}
