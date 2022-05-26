﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraduatedCylinder.Roslyn.Full;

[Generator]
public class ExtensionApiGenerator : BaseGenerator
{

    public ExtensionApiGenerator()
        : base("GraduatedCylinder") { }

    protected override void ExecuteInternal(GeneratorExecutionContext context) {
        if (context.SyntaxReceiver is not UnitReceiver receiver) {
            return;
        }
        //Debugger.Launch();

        if (!SetExternalGenerationPath(context)) {
            return;
        }

        foreach (EnumDeclarationSyntax @enum in receiver.GetUnits(context.Compilation)) {
            Log($"Generating Extensions for {@enum.Identifier}");
            GenerateExtensionsFor(@enum, context);
        }
    }

    protected override void InitializeInternal(GeneratorInitializationContext context) {
        context.RegisterForSyntaxNotifications(() => new UnitReceiver());
    }

    private void GenerateExtensionsFor(EnumDeclarationSyntax @enum, GeneratorExecutionContext context) {
        NameSet names = NameSet.FromUnitsType(@enum.Identifier.Text);
        SemanticModel semanticModel = context.Compilation.GetSemanticModel(@enum.SyntaxTree);
        string extensionDllName = $"{context.Compilation.AssemblyName}.Extensions";
        string filename = $"Source\\{extensionDllName}\\{names.DimensionTypeName}Extensions.g.cs";

        bool hasExtensions = false;
        WriteAutoGeneratedNotification();

        Buffer.AppendLine("#nullable enable");
        Buffer.AppendLine("using System;");
        Buffer.AppendLine("using System.Runtime.CompilerServices;");
        Buffer.AppendLine();
        Buffer.AppendLine("namespace GraduatedCylinder.Extensions;");
        Buffer.AppendLine();
        Buffer.AppendLine($"public static class {names.ExtensionsTypeName}");
        Buffer.AppendLine("{");

        foreach (EnumMemberDeclarationSyntax enumMember in @enum.Members) {
            ISymbol? enumValue = semanticModel.GetDeclaredSymbol(enumMember);
            AttributeData? attribute = enumValue?.GetAttributes()
                                                .SingleOrDefault(a => a.AttributeClass?.ContainingNamespace.ToDisplayString() ==
                                                                      "GraduatedCylinder.Extensions");
            if (attribute is null) { continue;}

            hasExtensions = true;
            string methodName = attribute.ConstructorArguments[0].Value!.ToString();

            Buffer.AppendLine("\t[MethodImpl(MethodImplOptions.AggressiveInlining)]");
            Buffer.AppendLine($"\tpublic static {names.DimensionTypeName} {methodName}(this int value) {{");
            Buffer.AppendLine($"\t\treturn new {names.DimensionTypeName}(value, {names.UnitsTypeName}.{enumValue?.Name});");
            Buffer.AppendLine("\t}");
            Buffer.AppendLine();
            Buffer.AppendLine("\t[MethodImpl(MethodImplOptions.AggressiveInlining)]");
            Buffer.AppendLine($"\tpublic static {names.DimensionTypeName} {methodName}(this long value) {{");
            Buffer.AppendLine($"\t\treturn new {names.DimensionTypeName}(value, {names.UnitsTypeName}.{enumValue?.Name});");
            Buffer.AppendLine("\t}");
            Buffer.AppendLine();
            Buffer.AppendLine("\t[MethodImpl(MethodImplOptions.AggressiveInlining)]");
            Buffer.AppendLine($"\tpublic static {names.DimensionTypeName} {methodName}(this float value) {{");
            Buffer.AppendLine($"\t\treturn new {names.DimensionTypeName}(value, {names.UnitsTypeName}.{enumValue?.Name});");
            Buffer.AppendLine("\t}");
            Buffer.AppendLine();
            Buffer.AppendLine("\t[MethodImpl(MethodImplOptions.AggressiveInlining)]");
            Buffer.AppendLine($"\tpublic static {names.DimensionTypeName} {methodName}(this double value) {{");
            Buffer.AppendLine($"\t\treturn new {names.DimensionTypeName}(value, {names.UnitsTypeName}.{enumValue?.Name});");
            Buffer.AppendLine("\t}");
            Buffer.AppendLine();

        }

        Buffer.AppendLine("}");
        Buffer.AppendLine();
        Buffer.AppendLine($"// Buffer.Capacity: {Buffer.Capacity}");
        Buffer.AppendLine($"// Buffer.Length: {Buffer.Length}");


        if (hasExtensions) {
            BufferToGeneratedFile(filename).SaveToDisk();
        } else {
            Buffer.Clear();
        }
    }

}