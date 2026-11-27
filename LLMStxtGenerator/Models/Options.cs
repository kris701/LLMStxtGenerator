using CommandLine;

namespace LLMStxtGenerator.Models
{
	public class Options
	{
		[Option('c', "config", Required = true, HelpText = "A JSON file with configurations for how to generate the llms.txt file")]
		public string ConfigFile { get; set; }

		[Option('o', "output", Required = true, HelpText = "Path to where to put the result")]
		public string OutputPath { get; set; }
	}
}
