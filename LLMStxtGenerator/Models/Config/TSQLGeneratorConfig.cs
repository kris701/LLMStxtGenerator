namespace LLMStxtGenerator.Models.Config
{
	/// <summary>
	/// T-SQL specific config file
	/// </summary>
	public class TSQLGeneratorConfig : BaseGeneratorConfig
	{
		public bool IncludeDeclaration { get; set; }
		public bool IncludeSummary { get; set; }
	}
}
