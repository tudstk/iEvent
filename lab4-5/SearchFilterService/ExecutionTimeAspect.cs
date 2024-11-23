using PostSharp.Aspects;
using System;
using System.Diagnostics;
using PostSharp.Serialization;

[PSerializable]
public class ExecutionTimeAspect : OnMethodBoundaryAspect
{
    private Stopwatch _stopwatch;

    public override void OnEntry(MethodExecutionArgs args)
    {
        _stopwatch = Stopwatch.StartNew();
        Console.WriteLine($"Start constructing search query...");
    }

    public override void OnExit(MethodExecutionArgs args)
    {
        _stopwatch.Stop();
        Console.WriteLine($"Search query constructed in {_stopwatch.ElapsedMilliseconds} ms.");
    }

    public override void OnException(MethodExecutionArgs args)
    {
        Console.WriteLine($"An error occurred while constructing search query: {args.Exception.Message}");
    }
}
