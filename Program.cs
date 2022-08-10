using System.Text;

namespace RoslynatorAnalyzerNRE;

// Run
// $ dotnet build -p:DebugAnalyzers=Roslynator.Analyzers -p:TreatWarningsAsErrors=false -p:RunAnalyzersDuringBuild=true /target:rebuild ./RoslynatorAnalyzerNRE/RoslynatorAnalyzerNRE.csproj
// Get
// CSC : warning AD0001: Analyzer 'Roslynator.CSharp.Analysis.InvocationExpressionAnalyzer' threw an exception of type 'System.NullReferenceException' with message 'Object reference not set to an instance of an object.'. [/home/user/Repro/RoslynatorAnalyzerNRE/RoslynatorAnalyzerNRE.csproj]
static class Program
{
    static void Main(string[] _)
    {
        var strBuilder = new StringBuilder();
        strBuilder
            .Append("Hello, World")
            .Append(DateTime.UtcNow.Millisecond % 2 == 0 ? "??" : '!'); // '!' makes the analyzer to detect a value type boxing warning, that in turn triggers the NRE

        Console.WriteLine(strBuilder.ToString());
    }
}
