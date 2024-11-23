using PostSharp.Aspects;
using PostSharp.Serialization;
using System;

[PSerializable]
public class StrategyAspect : OnMethodBoundaryAspect
{
    public override void OnEntry(MethodExecutionArgs args)
    {
        Console.WriteLine("Strategy for recommendation engine is being changed...");
    }

    public override void OnExit(MethodExecutionArgs args)
    {
        Console.WriteLine("Strategy change complete.");
    }
}
