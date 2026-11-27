using LLMStxtGenerator.Models.Config;

namespace LLMStxtGenerator.Services
{
	/// <summary>
	/// Generator for TS projects
	/// </summary>
	public class TypescriptGeneratorService : BaseGeneratorService<TypescriptGeneratorConfig>
	{
		public override async Task<string> Generate(TypescriptGeneratorConfig config)
		{
			throw new NotImplementedException();
		}
	}
}
