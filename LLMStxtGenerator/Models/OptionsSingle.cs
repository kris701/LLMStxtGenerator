using CommandLine;

namespace LLMStxtGenerator.Models
{
	/// <summary>
	/// The model used for CLI input
	/// </summary>
	[Verb("single", HelpText = "Run a single config file")]
	public class OptionsSingle
	{
		[Option('c', "config", Required = true, HelpText = "A JSON file with configurations for how to generate the llms.txt file")]
		public string ConfigFile { get; set; }
	}
}
