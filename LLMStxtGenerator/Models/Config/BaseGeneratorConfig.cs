namespace LLMStxtGenerator.Models.Config
{
	/// <summary>
	/// Base implementation of the generator config interface. Simply includes fields that all the sub implementations all have anyways.
	/// </summary>
	public abstract class BaseGeneratorConfig : IGeneratorConfig
	{
		public string Name { get; set; }

		public string OutputFile { get; set; }
		public string WorkingDirectory { get; set; }
		public List<string> IncludeGlobs { get; set; }
		public List<string> ExcludeGlobs { get; set; }
	}
}
