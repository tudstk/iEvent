using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Text;
using System.Globalization;
public class EventScraper : IDisposable
{
    private IWebDriver driver;
    private Monitor monitor;
    private List<string> actionLog;
    private PerformanceMonitor performanceMonitor;

    public EventScraper()
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless"); 
        options.AddArgument("--no-sandbox");
        driver = new ChromeDriver(options);
        monitor = new Monitor();
        actionLog = new List<string>();
        performanceMonitor = new PerformanceMonitor();
    }

    public List<EventData> ScrapeEvents(string url, string eventType, string genre = null)
    {
        List<EventData> events = new List<EventData>();

        try
        {
            performanceMonitor.Start();
            actionLog.Add("Navigate");
            driver.Navigate().GoToUrl(url);
            long pageLoadTime = performanceMonitor.Stop();
            Console.WriteLine($"Page load time: {pageLoadTime} ms");

            try
            {
                long cookieClickTime = performanceMonitor.Measure(() =>
                {
                    actionLog.Add("FindElement");
                    var cookieButton = driver.FindElement(By.CssSelector("a[aria-label='allow cookies']"));
                    actionLog.Add("Click");
                    cookieButton.Click();
                });
                Console.WriteLine($"Cookie click time: {cookieClickTime} ms");
            }
            catch (NoSuchElementException)
            {
                // Cookie banner not found, continue
            }

            bool moreButtonExists = true;
            while (moreButtonExists)
            {
                try
                {
                long moreButtonTime = performanceMonitor.Measure(() =>
                    {
                        actionLog.Add("FindElement");
                        var moreButton = driver.FindElement(By.CssSelector("div.btn-more-container a.btn-more"));
                        Actions actions = new Actions(driver);
                        actions.MoveToElement(moreButton).Perform();
                        actionLog.Add("Click");
                        moreButton.Click();
                    });
                    Console.WriteLine($"More button click time: {moreButtonTime} ms");
                }
                catch (NoSuchElementException)
                {
                    moreButtonExists = false;
                }
                catch (ElementNotInteractableException)
                {
                    break;
                }
            }

            actionLog.Add("FindElement");
            var eventNodes = driver.FindElements(By.CssSelector("div[data-event-list='item']"));

            foreach (var eventNode in eventNodes)
            {
                try
                {
                    string imageLink = "";
                    try
                    {
                        actionLog.Add("FindElement");
                        var imgElement = eventNode.FindElement(By.CssSelector("img"));
                        imageLink = imgElement.GetAttribute("src");
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine("Image link not found for event " + eventNode.Text);
                    }

                    actionLog.Add("FindElement");
                    string day = eventNode.FindElement(By.CssSelector("span.date-day")).Text;
                    actionLog.Add("FindElement");
                    string month = eventNode.FindElement(By.CssSelector("span.date-month")).Text;
                    string year = "2024"; 
                    try
                    {
                        actionLog.Add("FindElement");
                        year = eventNode.FindElement(By.CssSelector("span.date-year")).Text;
                    }
                    catch (NoSuchElementException)
                    {
                        // Default year remains
                    }
                    string fullDate = $"{day} {month} {year}";

                    actionLog.Add("FindElement");
                    string title = eventNode.FindElement(By.CssSelector("div.title span")).Text;

                    actionLog.Add("FindElement");
                    string location = eventNode.FindElement(By.CssSelector("div.location a:nth-of-type(2)")).Text;

                    string description = "";
                    try
                    {
                        actionLog.Add("FindElement");
                        description = eventNode.FindElement(By.CssSelector("div.main-info > div:not([class])")).Text;
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine("Description not found for event " + eventNode.Text);
                    }

                    if (description.Contains("Atenție: ultimele bilete"))
                    {
                        description = description.Replace("Atenție: ultimele bilete", "");
                    }
                    description += "...";

                    events.Add(new EventData
                    {
                        ImageLink = imageLink,
                        Date = fullDate,
                        Title = RemoveDiacritics(title),
                        Location = RemoveDiacritics(location),
                        Description = RemoveDiacritics(description),
                        EventType = eventType,
                        Genre = genre
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error processing an event: " + e.Message);
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        monitor.MonitorFSM(actionLog);
        monitor.MonitorERE(actionLog);
        monitor.MonitorLTL(actionLog);

        driver.Manage().Cookies.DeleteAllCookies();
        return events;
    }

    public void Dispose()
    {
        driver.Quit();
    }

    public static string RemoveDiacritics(string text)
    {
        if (text == null)
            return null;

        var normalizedString = text.Normalize(NormalizationForm.FormD);
        StringBuilder stringBuilder = new StringBuilder();
        foreach (char c in normalizedString)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                stringBuilder.Append(c);
        }
        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
}

public class EventData
{
    public string ImageLink { get; set; }
    public string Date { get; set; }
    public string Title { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public string EventType { get; set; } 
    public string Genre { get; set;}
}
