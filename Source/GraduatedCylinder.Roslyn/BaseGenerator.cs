﻿using System.Text;
using Microsoft.CodeAnalysis;

namespace GraduatedCylinder.Roslyn;

public abstract class BaseGenerator : ISourceGenerator
{

    protected BaseGenerator(params string[] generatorFor) {
        GeneratorFor = generatorFor;
    }

    public string? ExternalGenerationPath { get; private set; }

    /// <summary>
    /// Assembly Name for which this generator should run
    /// </summary>
    public string[] GeneratorFor { get; }

    protected StringBuilder Buffer { get; set; } = new StringBuilder(16384);

#if DEBUG
    private List<string> Logs { get; set; } = new();
#endif

    public void Execute(GeneratorExecutionContext context) {
        if (!GeneratorFor.Contains(context.Compilation.AssemblyName)) {
            return;
        }
        try {
#if DEBUG
            if (context.SyntaxReceiver is BaseReceiver receiver) {
                Logs = receiver.Logs;
            }
#endif
            Log($"Execute Started: {DateTime.Now:O}");
            ExecuteInternal(context);
        } catch (Exception e) {
            DiagnosticDescriptor descriptor =
                new DiagnosticDescriptor(GetType().Name, "Error", e.ToString(), "Error", DiagnosticSeverity.Error, true);
            Diagnostic diagnostic = Diagnostic.Create(descriptor, Location.None);
            context.ReportDiagnostic(diagnostic);
        } finally {
            Log($"Execute Finished: {DateTime.Now:O}");
#if DEBUG
            string logContent = $"/*\r\n{string.Join(Environment.NewLine, Logs)}\r\n*/";
            context.AddSource($"{GetType().Name}_Log.cs", logContent);
#endif
        }
    }

    public void Initialize(GeneratorInitializationContext context) {
        InitializeInternal(context);
    }

    protected GeneratedFile BufferToGeneratedFile(string fileName) {
        if (ExternalGenerationPath is not null) {
            fileName = ExternalGenerationPath + fileName;
        }
        Log($"//Buffer->File: {fileName}; Buffer.Length: {Buffer.Length}; Buffer.Capacity: {Buffer.Capacity}");
        GeneratedFile file = new GeneratedFile(fileName, Buffer.ToString());
        Buffer.Clear();

        return file;
    }

    protected abstract void ExecuteInternal(GeneratorExecutionContext context);

    protected abstract void InitializeInternal(GeneratorInitializationContext context);

    protected void Log(string value) {
#if DEBUG
        Logs.Add(value);
#endif
    }

    protected bool SetExternalGenerationPath(GeneratorExecutionContext context) {
        ExternalGenerationPath = Environment.GetEnvironmentVariable("GraduatedCylinder");
        if (ExternalGenerationPath is null) {
            DiagnosticDescriptor descriptor = new DiagnosticDescriptor("GC-ENV-01",
                                                                       "Build Environment Variable Missing",
                                                                       "Missing Environment Variable named 'GraduatedCylinder', please add it. It should have the value of the root directory of the repository.",
                                                                       "Error",
                                                                       DiagnosticSeverity.Error,
                                                                       true);
            Diagnostic diagnostic = Diagnostic.Create(descriptor, Location.None);
            context.ReportDiagnostic(diagnostic);
            return false;
        }
        if (!Directory.Exists(ExternalGenerationPath)) {
            DiagnosticDescriptor descriptor = new DiagnosticDescriptor("GC-ENV-02",
                                                                       "Build Environment Variable Invalid",
                                                                       "Invalid Environment Variable named 'GraduatedCylinder'. It should have the value of the root directory of the repository.",
                                                                       "Error",
                                                                       DiagnosticSeverity.Error,
                                                                       true);
            Diagnostic diagnostic = Diagnostic.Create(descriptor, Location.None);
            context.ReportDiagnostic(diagnostic);
            return false;
        }
        if (!ExternalGenerationPath.EndsWith("\\")) {
            ExternalGenerationPath += "\\";
        }
        return true;
    }

    protected void WriteAutoGeneratedNotification() {
        Buffer.AppendLine($"// This file was generated by {GetType().FullName} at {DateTime.Now:R}");
        Buffer.AppendLine("// Do not modify, your changes will be lost on the next compile.");
        Buffer.AppendLine();
    }

}