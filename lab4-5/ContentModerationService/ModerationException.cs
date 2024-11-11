using System;

public class ModerationException : Exception
{
    public ModerationException(string message) : base(message) { }
}