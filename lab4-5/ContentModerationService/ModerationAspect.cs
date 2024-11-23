using PostSharp.Aspects;
using PostSharp.Serialization;
using System;

[PSerializable]
public class ModerationAspect : OnMethodBoundaryAspect
{
    public override void OnEntry(MethodExecutionArgs args)
    {
        Console.WriteLine($"Moderation started for content: {args.Arguments[0]}");
    }

    public override void OnExit(MethodExecutionArgs args)
    {
        var content = args.Arguments[0] as Content;
        Console.WriteLine($"Moderation completed for content: {content?.IsApproved}");
    }

    public override void OnException(MethodExecutionArgs args)
    {
        Console.WriteLine($"Moderation failed for content: {args.Arguments[0]} due to {args.Exception.Message}");
    }
}
