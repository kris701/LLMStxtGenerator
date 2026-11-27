using CommandLine;

namespace LLMStxtGenerator.Models
{
	/// <summary>
	/// The model used for CLI input
	/// </summary>
	[Verb("multiple", HelpText = "Run multiple configs")]
	public class OptionsMultiple
	{
		[Option('c', "configs", Required = true, HelpText = "A set of JSON files with configurations for how to generate the llms.txt file")]
		public IEnumerable<string> ConfigFiles { get; set; }

		[Option('r', "rootllmstxt", Required = false, HelpText = "Optionally generate a llms.txt file that points to all the other config file outputs made here.")]
		public string? RootLLMsTxt { get; set; }
	}
}
