public class Content
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public bool IsApproved { get; set; } = false;

    public void CensorWord(string word)
    {
        string censor = new string('*', word.Length);
        Description = Description.Replace(word, censor, StringComparison.OrdinalIgnoreCase);
    }
}