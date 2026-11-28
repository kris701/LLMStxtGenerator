using System.Text.Json.Serialization;

namespace LLMStxtGenerator.Models.Config
{
	/// <summary>
	/// Interface for the generator config.
	/// This is a JSON polymorphic interface, so it has several subimplementations.
	/// The JSON discriminator is the standard '$type' property, where the value should be an integer, 0 for C# projects, 1 for TS projects and 2 for T-SQL projects.
	/// </summary>
	[JsonPolymorphic]
	[JsonDerivedType(typeof(CSharpGeneratorConfig), typeDiscriminator: (int)GeneratorTypes.CSharp)]
	[JsonDerivedType(typeof(TypescriptGeneratorConfig), typeDiscriminator: (int)GeneratorTypes.Typescript)]
	[JsonDerivedType(typeof(TSQLGeneratorConfig), typeDiscriminator: (int)GeneratorTypes.TSQL)]
	public interface IGeneratorConfig
	{
		public string Name { get; set; }

		public string OutputFile { get; set; }
		public string WorkingDirectory { get; set; }
		public List<string> IncludeGlobs { get; set; }
		public List<string> ExcludeGlobs { get; set; }
	}
}
