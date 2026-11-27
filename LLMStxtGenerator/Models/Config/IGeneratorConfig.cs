using System.Text.Json.Serialization;

namespace LLMStxtGenerator.Models.Config
{
	[JsonPolymorphic]
	[JsonDerivedType(typeof(CSharpGeneratorConfig), typeDiscriminator: (int)GeneratorTypes.CSharp)]
	[JsonDerivedType(typeof(TypescriptGeneratorConfig), typeDiscriminator: (int)GeneratorTypes.Typescript)]
	public interface IGeneratorConfig
	{
		public string WorkingDirectory { get; set; }
	}
}
