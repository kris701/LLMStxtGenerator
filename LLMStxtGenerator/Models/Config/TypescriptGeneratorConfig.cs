namespace LLMStxtGenerator.Models.Config
{
	/// <summary>
	/// TS specific config file
	/// </summary>
	public class TypescriptGeneratorConfig : BaseGeneratorConfig
	{
		public bool IncludeDeclaration { get; set; }
		public bool IncludeSummary { get; set; }
	}
}
