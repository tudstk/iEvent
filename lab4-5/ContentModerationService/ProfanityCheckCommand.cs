public class ProfanityCheckCommand : IModerationCommand
{
    public bool Execute(Content content)
    {
        // Example
        string[] profanities = { "badword1", "badword2" };

        foreach (string word in profanities)
        {
            if (content.Description.Contains(word, StringComparison.OrdinalIgnoreCase))
            {
                content.CensorWord(word);
                throw new ProfanityException("Profanity detected and censored.");
            }
        }
        return true;
    }
}