<p align="center">
    <img src="https://github.com/user-attachments/assets/f528651d-47f9-46f0-9544-ebf422cfd3e5" width="200" height="200" />
</p>

[![Build and Publish](https://github.com/kris701/LLMS-txt-Generator/actions/workflows/dotnet.yml/badge.svg)](https://github.com/kris701/LLMS-txt-Generator/actions/workflows/dotnet.yml)
![Nuget](https://img.shields.io/nuget/v/LLMS-txt-Generator)
![Nuget](https://img.shields.io/nuget/dt/LLMS-txt-Generator)
![GitHub last commit (branch)](https://img.shields.io/github/last-commit/kris701/LLMS-txt-Generator/main)
![GitHub commit activity (branch)](https://img.shields.io/github/commit-activity/m/kris701/LLMS-txt-Generator)
![Static Badge](https://img.shields.io/badge/Platform-Windows-blue)
![Static Badge](https://img.shields.io/badge/Platform-Linux-blue)
![Static Badge](https://img.shields.io/badge/Framework-dotnet--10.0-green)

# LLMS.txt Generator
This is a simple little CLI tool to generate a `llms.txt` file for your C# or Typescript project.
You simply give it a config file for your project, run the tool and it will generate a `llms.txt`
file that contains overview information, such as what files are where, what their declarations are
and a summary if the file has one.

## Installation

The project is packages as a dotnet tool, so simply install it by writing:

```
dotnet tool install LLMStxtGenerator -g
```
in a terminal.

You can also find it on the [NuGet Package Manager](https://www.nuget.org/packages/LLMStxtGenerator/) yourself.

## Usage

As an example, you can take this project and generate a `llms.txt` file.
By using the config:
```json
{
	"$type":0,
	"Name":"C# Config",
	"OutputFile":"llms.txt",
	"WorkingDirectory":".",
	"IncludeGlobs":[
		"**.cs",
	],
	"ExcludeGlobs":[
		"**/bin",
		"**/obj"
	],
	"IncludeDeclaration":true,
	"IncludeSummary":true
}
```

And running it with `llmstxtgenerator single -c config.json`.
It will generate a `llms.txt` file that looks like this:

```txt
There are a total of 11 files relevant for you.
You should consider only these files, unless told otherwise.
  * "LLMStxtGenerator/Program.cs". Declaration: "internal class Program". Summary: "Entry CLI program"
   * "LLMStxtGenerator/Models/Options.cs". Declaration: "public class Options". Summary: "The model used for CLI input"
    * "LLMStxtGenerator/Models/Config/BaseGeneratorConfig.cs". Declaration: "abstract class BaseGeneratorConfig". Summary: "Base implementation of the generator config interface. Simply includes fields that all the sub implementations all have anyways."
    * "LLMStxtGenerator/Models/Config/CSharpGeneratorConfig.cs". Declaration: "public class CSharpGeneratorConfig". Summary: "C# specific config file"
    * "LLMStxtGenerator/Models/Config/GeneratorTypes.cs". Declaration: "public enum GeneratorTypes". Summary: "Enum of what generator types there are"
    * "LLMStxtGenerator/Models/Config/IGeneratorConfig.cs". Declaration: "public interface IGeneratorConfig". Summary: "Interface for the generator config. This is a JSON polymorphic interface, so it has several subimplementations. The JSON discriminator is the standard '$type' property, where the value should be an integer, 0 for C# projects and 1 for TS projects."
    * "LLMStxtGenerator/Models/Config/TypescriptGeneratorConfig.cs". Declaration: "public class TypescriptGeneratorConfig". Summary: "TS specific config file"
   * "LLMStxtGenerator/Services/BaseGeneratorService.cs". Declaration: "abstract class BaseGeneratorService". Summary: "Base implementation of the generator service interface. Contains helper methods all the generators use."
   * "LLMStxtGenerator/Services/CSharpGeneratorService.cs". Declaration: "public class CSharpGeneratorService". Summary: "Generator for C# projects"
   * "LLMStxtGenerator/Services/IGeneratorService.cs". Declaration: "public interface IGeneratorService". Summary: "Interface for the generator services"
   * "LLMStxtGenerator/Services/TypescriptGeneratorService.cs". Declaration: "public class TypescriptGeneratorService". Summary: "Generator for TS projects"
```

Which your LLM can then injest, instead of having it look through the entire file structure itself.

You can also do multiple configs at the same time, in case you want multiple `llms.txt` files.
All you have to do is write `llmstxtgenerator multiple -c config1.json config2.json config3.json`
