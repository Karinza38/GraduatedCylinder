﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraduatedCylinder.Roslyn.Full;

[Generator]
public class ParsingGenerator : BaseGenerator
{

    public ParsingGenerator()
        : base("GraduatedCylinder") { }

    protected override void ExecuteInternal(GeneratorExecutionContext context) {
        if (context.SyntaxReceiver is not DimensionReceiver receiver) {
            return;
        }
        //Debugger.Launch();

        foreach (StructDeclarationSyntax @struct in receiver.GetDimensions(context.Compilation)) {
            Log($"Generating for {@struct.Identifier}");
            NameSet names = NameSet.FromDimensionType(@struct.Identifier.ToString());

            WriteAutoGeneratedNotification();

            Buffer.AppendLine("using System;");
            Buffer.AppendLine("using System.Text.RegularExpressions;");
            Buffer.AppendLine("using GraduatedCylinder.Text;");
            Buffer.AppendLine();
            Buffer.AppendLine("namespace GraduatedCylinder;");
            Buffer.AppendLine();
            Buffer.AppendLine($"public partial struct {names.DimensionTypeName}");
            Buffer.AppendLine("{");

            Buffer.AppendLine();
            Buffer.AppendLine($"\tpublic static {names.DimensionTypeName} Parse(string valueWithUnits) {{");
            Buffer.AppendLine("\t\tMatch match = Regexs.Pair.Match(valueWithUnits);");
            Buffer.AppendLine("\t\tif (match.Success) {");
            Buffer.AppendLine("\t\t\tif (float.TryParse(match.Groups[\"value\"].Value, out float floatValue)) {");
            Buffer.AppendLine(
                $"\t\t\t\t{names.UnitsTypeName} units = ShortNames.Get{names.UnitsTypeName}(match.Groups[\"units\"].Value);");
            Buffer.AppendLine($"\t\t\t\treturn new {names.DimensionTypeName}(floatValue, units);");
            Buffer.AppendLine("\t\t\t}");
            Buffer.AppendLine("\t\t}");
            Buffer.AppendLine("\t\tthrow new Exception($\"Error parsing: {valueWithUnits}\");");
            Buffer.AppendLine("\t}");

            Buffer.AppendLine();
            Buffer.AppendLine(
                $"\tpublic static {names.DimensionTypeName} Parse(string value, {names.UnitsTypeName} units) {{");
            Buffer.AppendLine("\t\tMatch match = Regexs.ValueOnly.Match(value);");
            Buffer.AppendLine("\t\tif (match.Success) {");
            Buffer.AppendLine("\t\t\tif (float.TryParse(match.Groups[0].Value, out float floatValue)) {");
            Buffer.AppendLine($"\t\t\t\treturn new {names.DimensionTypeName}(floatValue, units);");
            Buffer.AppendLine("\t\t\t}");
            Buffer.AppendLine("\t\t}");
            Buffer.AppendLine("\t\tthrow new Exception($\"Error parsing: {value}\");");
            Buffer.AppendLine("\t}");

            Buffer.AppendLine();
            Buffer.AppendLine("}");

            string filename = $"{names.DimensionTypeName}.Parse.g.cs";
            BufferToGeneratedFile(filename).AddToContext(context);
        }
    }

    protected override void InitializeInternal(GeneratorInitializationContext context) {
        context.RegisterForSyntaxNotifications(() => new DimensionReceiver());
    }

}