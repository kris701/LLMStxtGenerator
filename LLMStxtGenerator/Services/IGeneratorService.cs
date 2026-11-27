using LLMStxtGenerator.Models.Config;

namespace LLMStxtGenerator.Services
{
	public interface IGeneratorService<T> where T : IGeneratorConfig
	{
		public Task<string> Generate(T config);
	}
}
