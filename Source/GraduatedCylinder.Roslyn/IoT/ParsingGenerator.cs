﻿using System;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraduatedCylinder.Roslyn.IoT
{
    [Generator]
    public class ParsingGenerator : BaseGenerator
    {

        public ParsingGenerator()
            : base("GraduatedCylinder.IoT") { }

        protected override void ExecuteInternal(GeneratorExecutionContext context) {
            if (context.SyntaxReceiver is not DimensionReceiver receiver) {
                return;
            }
            //Debugger.Launch();

            string sourceRoot = @"C:\GSP-Projects\GraduatedCylinder\Source\GraduatedCylinder.IoT.Parsing";
            string filename = $"{sourceRoot}\\Parser.g.cs";

            Buffer.AppendLine("using System;");
            Buffer.AppendLine("using System.Text.RegularExpressions;");
            Buffer.AppendLine("using GraduatedCylinder.IoT.Abbreviations;");
            Buffer.AppendLine("");
            Buffer.AppendLine("namespace GraduatedCylinder.IoT.Parsing");
            Buffer.AppendLine("{");
            Buffer.AppendLine("\tpublic static partial class Parser");
            Buffer.AppendLine("\t{");

            foreach (StructDeclarationSyntax @struct in receiver.Structs) {
                Log($"Generating for {@struct.Identifier}");
                NameSet names = NameSet.FromDimensionType(@struct.Identifier.ToString());

                Buffer.AppendLine("");
                Buffer.AppendLine($"\t\tpublic static {names.DimensionTypeName} Parse{names.DimensionTypeName}(string value, {names.UnitsTypeName} defaultUnits) {{");
                Buffer.AppendLine("\t\t\tMatch match = PairRegex.Match(value);");
                Buffer.AppendLine("\t\t\tif (match.Success) {");
                Buffer.AppendLine("\t\t\t\tif (float.TryParse(match.Groups[\"value\"].Value, out float floatValue)) {");
                Buffer.AppendLine($"\t\t\t\t\t{names.UnitsTypeName} units = ShortNames.Get{names.UnitsTypeName}(match.Groups[\"units\"].Value);");
                Buffer.AppendLine($"\t\t\t\t\treturn new {names.DimensionTypeName}(floatValue, units);");
                Buffer.AppendLine("\t\t\t\t}");
                Buffer.AppendLine("\t\t\t\tthrow new Exception($\"Error parsing: {value}\");");
                Buffer.AppendLine("\t\t\t}");
                Buffer.AppendLine("\t\t\tmatch = ValueOnlyRegex.Match(value);");
                Buffer.AppendLine("\t\t\tif (match.Success) {");
                Buffer.AppendLine("\t\t\t\tif (float.TryParse(match.Groups[0].Value, out float floatValue)) {");
                Buffer.AppendLine($"\t\t\t\t\treturn new {names.DimensionTypeName}(floatValue, defaultUnits);");
                Buffer.AppendLine("\t\t\t\t}");
                Buffer.AppendLine("\t\t\t}");
                Buffer.AppendLine("\t\t\tthrow new Exception($\"Error parsing: {value}\");");
                Buffer.AppendLine("\t\t}");
            }

            Buffer.AppendLine("\t}");
            Buffer.AppendLine("}");

            //don't add to the IoT.dll
            GeneratedFile generatedFile = BufferToGeneratedFile(filename);
            File.WriteAllText(generatedFile.FileName, generatedFile.Content);
        }

        protected override void InitializeInternal(GeneratorInitializationContext context) {
            context.RegisterForSyntaxNotifications(() => new DimensionReceiver());
        }

    }
}