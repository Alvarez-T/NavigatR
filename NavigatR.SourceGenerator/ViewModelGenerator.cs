using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace NavigatR.SourceGenerator;

using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

[Generator]
public class ViewBindGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context) { }

    public void Execute(GeneratorExecutionContext context)
    {
        // Get the compilation unit
        var syntaxTrees = context.Compilation.SyntaxTrees;

        foreach (var tree in syntaxTrees)
        {
            var semanticModel = context.Compilation.GetSemanticModel(tree);
            var root = tree.GetRoot();

            // Find all class declarations
            var classDeclarations = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

            foreach (var classDeclaration in classDeclarations)
            {
                // Check if the class inherits from 'View'
                var classSymbol = semanticModel.GetDeclaredSymbol(classDeclaration) as INamedTypeSymbol;
                if (classSymbol == null) continue;

                var baseType = classSymbol.BaseType;
                while (baseType != null)
                {
                    if (baseType.ToDisplayString() == "NavigatR.Avalonia")
                    {
                        // Find the InitializeComponent method
                        var methodDeclaration = classDeclaration.Members
                            .OfType<MethodDeclarationSyntax>()
                            .FirstOrDefault(m => m.Identifier.Text == "InitializeComponent");

                        if (methodDeclaration != null)
                        {
                            // Create the additional statement to inject
                            var additionalStatement = SyntaxFactory.ParseStatement("NavigatR.Avalonia.DataContextExtensions.BindViewModelToTopLevelControl(this);");
                            

                            // Inject the additional statement into the method body
                            var newBody = methodDeclaration.Body.AddStatements(additionalStatement);

                            var newMethodDeclaration = methodDeclaration.WithBody(newBody);
                            var newClassDeclaration = classDeclaration.ReplaceNode(methodDeclaration, newMethodDeclaration);
                            var newRoot = root.ReplaceNode(classDeclaration, newClassDeclaration);

                            // Add the modified syntax tree to the compilation
                            context.AddSource($"{classDeclaration.Identifier.Text}_Incremental.g.cs", SourceText.From(newRoot.NormalizeWhitespace().ToFullString(), Encoding.UTF8));
                        }
                        break;
                    }
                    baseType = baseType.BaseType;
                }
            }
        }
    }
}

/*
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
*/