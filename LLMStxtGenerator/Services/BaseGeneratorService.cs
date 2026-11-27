using LLMStxtGenerator.Models.Config;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

namespace LLMStxtGenerator.Services
{
	/// <summary>
	/// Base implementation of the generator service interface. Contains helper methods all the generators use.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class BaseGeneratorService<T> : IGeneratorService<T>
		where T : IGeneratorConfig
	{
		public abstract Task<string> Generate(T config);

		internal List<string> GetMatchingFiles(T config)
		{
			Matcher matcher = new();
			matcher.AddIncludePatterns(config.IncludeGlobs);
			matcher.AddExcludePatterns(config.ExcludeGlobs);

			PatternMatchingResult result = matcher.Execute(
				new DirectoryInfoWrapper(
					new DirectoryInfo(config.WorkingDirectory)));

			return result.Files.Select(x => x.Path).ToList();
		}

		internal int GetFileDepth(string path) => path.Count(x => x == '/');
	}
}
