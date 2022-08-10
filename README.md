# RoslynatorAnalyzerNRE
Repro of NullReferenceException in Roslynator.Anayzers

Run:
$ dotnet build -p:DebugAnalyzers=Roslynator.Analyzers -p:TreatWarningsAsErrors=false -p:RunAnalyzersDuringBuild=true /target:rebuild ./RoslynatorAnalyzerNRE/RoslynatorAnalyzerNRE.csproj

Get:
CSC : warning AD0001: Analyzer 'Roslynator.CSharp.Analysis.InvocationExpressionAnalyzer' threw an exception of type 'System.NullReferenceException' with message 'Object reference not set to an instance of an object.'. [/home/user/Repro/RoslynatorAnalyzerNRE/RoslynatorAnalyzerNRE.csproj]

Trigger: this expression
DateTime.UtcNow.Millisecond % 2 == 0 ? "??" : '!'

'!' is a value type that is boxed before being converted to string.
The analyzer fails at this point: https://github.com/JosefPihrt/Roslynator/blob/6f0bdbc4ba21f6ea1c570b92ac18baa55919ab71/src/Analyzers/CSharp/Analysis/AvoidBoxingOfValueTypeAnalysis.cs#L41

  Microsoft.CSharp.Core.targets(75, 5): Object reference not set to an instance of an object.
  Microsoft.CSharp.Core.targets(75, 5):    at Roslynator.CSharp.Analysis.AvoidBoxingOfValueTypeAnalysis.Analyze(SyntaxNodeAnalysisContext context, SimpleMemberInvocationExpressionInfo& invocationInfo) in /home/sergepavlov/Downloads/Roslynator/Roslynator-4.1.1/src/Analyzers/CSharp/Analysis/AvoidBoxingOfValueTypeAnalysis.cs:line 41
  Microsoft.CSharp.Core.targets(75, 5):    at Roslynator.CSharp.Analysis.InvocationExpressionAnalyzer.AnalyzeInvocationExpression(SyntaxNodeAnalysisContext context) in /home/sergepavlov/Downloads/Roslynator/Roslynator-4.1.1/src/Analyzers/CSharp/Analysis/InvocationExpressionAnalyzer.cs:line 450
  Microsoft.CSharp.Core.targets(75, 5):    at Roslynator.CSharp.Analysis.InvocationExpressionAnalyzer.AnalyzeInvocationExpression(SyntaxNodeAnalysisContext context) in /home/sergepavlov/Downloads/Roslynator/Roslynator-4.1.1/src/Analyzers/CSharp/Analysis/InvocationExpressionAnalyzer.cs:line 450
  Microsoft.CSharp.Core.targets(75, 5):    at Roslynator.CSharp.Analysis.InvocationExpressionAnalyzer.<>c.<Initialize>b__3_0(SyntaxNodeAnalysisContext f) in /home/sergepavlov/Downloads/Roslynator/Roslynator-4.1.1/src/Analyzers/CSharp/Analysis/InvocationExpressionAnalyzer.cs:line 59

