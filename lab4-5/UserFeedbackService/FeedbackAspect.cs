using PostSharp.Aspects;
using System;
using System.Diagnostics;
using PostSharp.Serialization;

[PSerializable]
public class FeedbackAspect : OnMethodBoundaryAspect
{
    public override void OnEntry(MethodExecutionArgs args)
    {
        Console.WriteLine($"Executing method: {args.Method.Name}.");
    }

    public override void OnExit(MethodExecutionArgs args)
    {
        Console.WriteLine($"Method {args.Method.Name} executed successfully.");
    }

    public override void OnException(MethodExecutionArgs args)
    {
        Console.WriteLine($"Exception occurred in method: {args.Method.Name}. Exception: {args.Exception.Message}");
    }
}
