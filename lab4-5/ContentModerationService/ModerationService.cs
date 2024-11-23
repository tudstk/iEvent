public class ModerationService
{
    private readonly ModerationHandler _handlerChain;

    public ModerationService()
    {
        var profanityCheckHandler = new CommandHandler(new ProfanityCheckCommand());
        var spamCheckHandler = new CommandHandler(new SpamCheckCommand());

        profanityCheckHandler.SetNext(spamCheckHandler);

        _handlerChain = profanityCheckHandler;
    }

    [ModerationAspect]
    public bool ModerateContent(Content content)
    {
        try
        {
            bool isApproved = _handlerChain.Handle(content);
            content.IsApproved = isApproved;
            return isApproved;
        }
        catch (ModerationException ex)
        {
            content.IsApproved = false;
            
            Console.WriteLine("Moderation failed: " + ex.Message);
            return false;
        }
    }
}
