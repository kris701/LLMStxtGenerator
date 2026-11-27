namespace LLMStxtGenerator.Models.Config
{
	/// <summary>
	/// C# specific config file
	/// </summary>
	public class CSharpGeneratorConfig : BaseGeneratorConfig
	{
		public bool IncludeDeclaration { get; set; }
		public bool IncludeSummary { get; set; }
	}
}
