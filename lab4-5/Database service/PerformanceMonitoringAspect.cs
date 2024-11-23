using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Diagnostics;

[PSerializable]
public class PerformanceMonitoringAspect : OnMethodBoundaryAspect
{
    private Stopwatch _stopwatch;

    public override void OnEntry(MethodExecutionArgs args)
    {
        _stopwatch = Stopwatch.StartNew();
        Console.WriteLine($"Started method {args.Method.Name}");
    }

    public override void OnExit(MethodExecutionArgs args)
    {
        _stopwatch.Stop();
        Console.WriteLine($"Method {args.Method.Name} executed in {_stopwatch.ElapsedMilliseconds} ms");
    }
}
