using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PlantUmlClassDiagramGenerator.Library
{
    public class TypeNameText
    {
        public string Identifier { get; set; }

        public string TypeArguments { get; set; }
        
        public static TypeNameText From(SimpleNameSyntax syntax)
        {
            var identifier = syntax.Identifier.Text;
            var typeArgs = string.Empty;
            if (syntax is GenericNameSyntax genericName && genericName.TypeArgumentList != null)
            {
                var count = genericName.TypeArgumentList.Arguments.Count;
                typeArgs = "<" + string.Join(",", genericName.TypeArgumentList.Arguments) + ">";
                identifier = $"\"{identifier}{typeArgs}\"";
            }
            else if (identifier.StartsWith("@"))
            {
                identifier = $"\"{identifier}\"";
            }
            return new TypeNameText
            {
                Identifier = identifier,
                TypeArguments = typeArgs
            };
        }

        public static TypeNameText From(GenericNameSyntax syntax)
        {
            int paramCount = syntax.TypeArgumentList.Arguments.Count;
            string[] parameters = new string[paramCount];
            if (paramCount > 1)
            {
                for (int i = 0; i < paramCount; i++)
                {
                    parameters[i] = $"T{i + 1}";
                }

            }
            else
            {
                parameters[0] = "T";
            }

            var typeArgs = "<" + string.Join(",", parameters) + ">";
            return new TypeNameText
            {
                Identifier = $"\"{syntax.Identifier.Text}{typeArgs}\"",
                TypeArguments = typeArgs,
            };
        }

        public static TypeNameText From(BaseTypeDeclarationSyntax syntax)
        {
            var identifier = syntax.Identifier.Text;
            var typeArgs = string.Empty;
            if (syntax is TypeDeclarationSyntax typeDeclaration && typeDeclaration.TypeParameterList != null)
            {
                var count = typeDeclaration.TypeParameterList.Parameters.Count;
                typeArgs = "<" + string.Join(",", typeDeclaration.TypeParameterList.Parameters) + ">";
                identifier = $"\"{identifier}{typeArgs}\"";
            }
            else if (identifier.StartsWith("@"))
            {
                identifier = $"\"{identifier}\"";
            }
            return new TypeNameText
            {
                Identifier = identifier,
                TypeArguments = typeArgs
            };
        }
    }
}
