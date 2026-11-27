using LLMStxtGenerator.Models.Config;

namespace LLMStxtGenerator.Services
{
	/// <summary>
	/// Interface for the generator services
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IGeneratorService<T> where T : IGeneratorConfig
	{
		public Task<string> Generate(T config);
	}
}
