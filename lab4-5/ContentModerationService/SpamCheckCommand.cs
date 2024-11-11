public class SpamCheckCommand : IModerationCommand
{
    public bool Execute(Content content)
    {
        if (content.Description.Length > 1000)
        {
            throw new SpamException("Content is considered spam due to excessive length.");
        }
        return true;
    }
}