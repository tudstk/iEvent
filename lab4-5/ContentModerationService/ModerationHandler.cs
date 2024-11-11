public abstract class ModerationHandler
{
    protected ModerationHandler NextHandler;

    public void SetNext(ModerationHandler nextHandler)
    {
        NextHandler = nextHandler;
    }

    public virtual bool Handle(Content content)
    {
        if (NextHandler != null)
        {
            return NextHandler.Handle(content);
        }
        return true;
    }
}