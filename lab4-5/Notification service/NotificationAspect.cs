using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Diagnostics;
using NotificationService;

[PSerializable]
public class NotificationAspect : OnMethodBoundaryAspect
{
    private Stopwatch _stopwatch;

    public override void OnEntry(MethodExecutionArgs args)
    {
        Console.WriteLine($"Sending notification of type '{args.Arguments[0]}' to {args.Arguments[2]}.");
        _stopwatch = Stopwatch.StartNew();
    }

    public override void OnExit(MethodExecutionArgs args)
    {
        _stopwatch.Stop();
        var notification = args.Arguments[0] as Notification;
        Console.WriteLine($"Notification sent. Duration: {_stopwatch.ElapsedMilliseconds} ms.");
    }

    public override void OnException(MethodExecutionArgs args)
    {
        Console.WriteLine($"Failed to send notification: {args.Exception.Message}");
    }
}
