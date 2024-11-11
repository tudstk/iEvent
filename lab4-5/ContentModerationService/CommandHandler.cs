public class CommandHandler : ModerationHandler
{
    private readonly IModerationCommand _command;

    public CommandHandler(IModerationCommand command)
    {
        _command = command;
    }

    public override bool Handle(Content content)
    {
        try
        {
            if (!_command.Execute(content))
            {
                return false;
            }
        }
        catch (ModerationException ex)
        {
            Console.WriteLine(ex.Message);

            //return false;
        }
        return base.Handle(content);
    }
}