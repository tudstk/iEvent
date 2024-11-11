using System;

public class ModerationServiceTests
{
    public static void Main()
    {
        RunTests();
    }

    public static void RunTests()
    {
        TestContentWithoutIssues();
        TestContentWithProfanity();
        TestContentWithSpam();
        TestContentWithBothProfanityAndSpam();
        TestContentNearSpamBoundary();
        TestContentWithMultipleProfanityInstances();
    }

    // Test Case 1: Content without any issues
    public static void TestContentWithoutIssues()
    {
        var content = new Content
        {
            Title = "Clean Content",
            Description = "This is a clean description without any profanity or spam.",
            Author = "Author1"
        };

        var moderationService = new ModerationService();
        bool isContentApproved = moderationService.ModerateContent(content);

        Console.WriteLine("TestContentWithoutIssues:");
        Console.WriteLine($"Content approved: {isContentApproved}");
        Console.WriteLine($"Censored Description: {content.Description}");
        Console.WriteLine();
    }

    // Test Case 2: Content with profanity
    public static void TestContentWithProfanity()
    {
        var content = new Content
        {
            Title = "Profane Content",
            Description = "This contains badword1, which should be censored.",
            Author = "Author2"
        };

        var moderationService = new ModerationService();
        bool isContentApproved = moderationService.ModerateContent(content);

        Console.WriteLine("TestContentWithProfanity:");
        Console.WriteLine($"Content approved: {isContentApproved}");
        Console.WriteLine($"Censored Description: {content.Description}");
        Console.WriteLine();
    }

    // Test Case 3: Content flagged as spam due to length
    public static void TestContentWithSpam()
    {
        var content = new Content
        {
            Title = "Spam Content",
            Description = new string('A', 1001),
            Author = "Author3"
        };

        var moderationService = new ModerationService();
        bool isContentApproved = moderationService.ModerateContent(content);

        Console.WriteLine("TestContentWithSpam:");
        Console.WriteLine($"Content approved: {isContentApproved}");
        Console.WriteLine($"Description: {content.Description}");
        Console.WriteLine();
    }

    // Test Case 4: Content with both profanity and spam
    public static void TestContentWithBothProfanityAndSpam()
    {
        var content = new Content
        {
            Title = "Profane and Spam Content",
            Description = "This description contains badword2 and is also very long, so it should fail both checks." + new string('A', 950),
            Author = "Author4"
        };

        var moderationService = new ModerationService();
        bool isContentApproved = moderationService.ModerateContent(content);

        Console.WriteLine("TestContentWithBothProfanityAndSpam:");
        Console.WriteLine($"Content approved: {isContentApproved}");
        Console.WriteLine($"Censored Description: {content.Description}");
        Console.WriteLine();
    }

    // Test Case 5: Content near the spam boundary (499 characters)
    public static void TestContentNearSpamBoundary()
    {
        var content = new Content
        {
            Title = "Boundary Spam Content",
            Description = new string('B', 999),
            Author = "Author5"
        };

        var moderationService = new ModerationService();
        bool isContentApproved = moderationService.ModerateContent(content);

        Console.WriteLine("TestContentNearSpamBoundary:");
        Console.WriteLine($"Content approved: {isContentApproved}");
        Console.WriteLine($"Description: {content.Description}");
        Console.WriteLine();
    }

    // Test Case 6: Content with multiple instances of profanity
    public static void TestContentWithMultipleProfanityInstances()
    {
        var content = new Content
        {
            Title = "Multiple Profanity Content",
            Description = "This badword1 content has multiple badword1 instances of badword2 profanity.",
            Author = "Author6"
        };

        var moderationService = new ModerationService();
        bool isContentApproved = moderationService.ModerateContent(content);

        Console.WriteLine("TestContentWithMultipleProfanityInstances:");
        Console.WriteLine($"Content approved: {isContentApproved}");
        Console.WriteLine($"Censored Description: {content.Description}");
        Console.WriteLine();
    }
}
