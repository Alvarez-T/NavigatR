using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace NavigatR.SourceGenerator;

[Generator]
public class ViewModelGenerator : ISourceGenerator
{
    private const string InterfaceName = "IViewModel";

    public void Initialize(GeneratorInitializationContext context)
    {
        throw new NotImplementedException();
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var syntaxTrees = context.Compilation.SyntaxTrees;

        // Collect classes that have "ViewModel" in their name
        var viewModelClasses = new List<ClassDeclarationSyntax>();
        foreach (var tree in syntaxTrees)
        {
            var modelDeclarations = tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>()
                .Where(c => c.Identifier.ValueText.EndsWith("ViewModel", StringComparison.OrdinalIgnoreCase));

            viewModelClasses.AddRange(modelDeclarations);
        }

        // Generate the implementation of IViewModel for each class
        foreach (var viewModelClass in viewModelClasses)
        {
            var generatedCode = GenerateImplementationCode(viewModelClass);
            var sourceText = SourceText.From(generatedCode, System.Text.Encoding.UTF8);

            // Add the generated source code to the compilation
            context.AddSource($"{viewModelClass.Identifier.ValueText}Implementation.cs", sourceText);
        }
    }

    private string GenerateImplementationCode(ClassDeclarationSyntax classSyntax)
    {
        var className = classSyntax.Identifier.ValueText;
        var generatedCode = $@"
            using System;
            
            namespace {classSyntax.Parent?.ChildNodes().OfType<NamespaceDeclarationSyntax>().First().Name}
            {{
                public partial class {className} : {InterfaceName}
                {{
                    // Implement members of {InterfaceName} here
                }}
            }}
        ";

        return generatedCode;
    }


}