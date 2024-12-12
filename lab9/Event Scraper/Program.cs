using System;
class Program
{
    static void Main(string[] args)
    {
        using (var scraper = new EventScraper())
        {
            List<EventData> theatreEvents = scraper.ScrapeEvents("https://www.iabilet.ro/bilete-teatru/", "Theatre");
            List<EventData> festivalEvents = scraper.ScrapeEvents("https://www.iabilet.ro/bilete-festivaluri/", "Festival");
            List<EventData> standupEvents = scraper.ScrapeEvents("https://www.iabilet.ro/bilete-stand-up-comedy/", "Stand-up Comedy");
            List<EventData> kidsEvents = scraper.ScrapeEvents("https://www.iabilet.ro/bilete-spectacole-pentru-copii/", "Kids");
            List<EventData> expoEvents = scraper.ScrapeEvents("https://www.iabilet.ro/bilete-expo-muzee/", "Expo");
            List<EventData> conferenceEvents = scraper.ScrapeEvents("https://www.iabilet.ro/bilete-conferinte/", "Conference");

            List<EventData> concertEvents = new List<EventData>();

            concertEvents.AddRange(scraper.ScrapeEvents("https://www.iabilet.ro/bilete-blues/", "Concert", "Blues"));
            concertEvents.AddRange(scraper.ScrapeEvents("https://www.iabilet.ro/bilete-concerte-muzica-clasica/", "Concert", "Classical Music"));
            concertEvents.AddRange(scraper.ScrapeEvents("https://www.iabilet.ro/bilete-colinde/", "Concert", "Colinde"));
            concertEvents.AddRange(scraper.ScrapeEvents("https://www.iabilet.ro/bilete-concerte-electro/", "Concert", "Electro"));
            concertEvents.AddRange(scraper.ScrapeEvents("https://www.iabilet.ro/bilete-concerte-folk/", "Concert", "Folk"));
            concertEvents.AddRange(scraper.ScrapeEvents("https://www.iabilet.ro/bilete-concerte-hip-hop/", "Concert", "Hip-Hop"));
            concertEvents.AddRange(scraper.ScrapeEvents("https://www.iabilet.ro/bilete-concerte-jazz/", "Concert", "Jazz"));
            concertEvents.AddRange(scraper.ScrapeEvents("https://www.iabilet.ro/bilete-manele/", "Concert", "Manele"));
            concertEvents.AddRange(scraper.ScrapeEvents("https://www.iabilet.ro/bilete-concerte-metal/", "Concert", "Metal"));
            concertEvents.AddRange(scraper.ScrapeEvents("https://www.iabilet.ro/bilete-muzica-de-petrecere/", "Concert", "Party Music"));
            concertEvents.AddRange(scraper.ScrapeEvents("https://www.iabilet.ro/bilete-concerte-populara/", "Concert", "Popular"));
            concertEvents.AddRange(scraper.ScrapeEvents("https://www.iabilet.ro/bilete-concerte-pop/", "Concert", "Pop"));
            concertEvents.AddRange(scraper.ScrapeEvents("https://www.iabilet.ro/bilete-concerte-pop-rock/", "Concert", "Pop-Rock"));
            concertEvents.AddRange(scraper.ScrapeEvents("https://www.iabilet.ro/bilete-concerte-rock/", "Concert", "Rock"));
            concertEvents.AddRange(scraper.ScrapeEvents("https://www.iabilet.ro/bilete-trap/", "Concert", "Trap"));
            concertEvents.AddRange(scraper.ScrapeEvents("https://www.iabilet.ro/bilete-world-music/", "Concert", "World Music"));
            
            HashSet<string> locations = new HashSet<string>();
            HashSet<string> genres = new HashSet<string>();
            HashSet<string> eventTypes = new HashSet<string>();
            
            foreach (var evt in concertEvents.Concat(theatreEvents).Concat(standupEvents).Concat(kidsEvents).Concat(expoEvents).Concat(festivalEvents).Concat(conferenceEvents))
            {
                locations.Add(evt.Location);
                if (!string.IsNullOrEmpty(evt.Genre))
                {
                    genres.Add(evt.Genre);
                }
                eventTypes.Add(evt.EventType);
            }

            Console.WriteLine("Unique Locations:");
            foreach (var location in locations)
            {
                Console.WriteLine(location);
            }

            Console.WriteLine("\nUnique Genres:");
            foreach (var genre in genres)
            {
                Console.WriteLine(genre);
            }

            Console.WriteLine("\nUnique Event Types:");
            foreach (var eventType in eventTypes)
            {
                Console.WriteLine(eventType);
            }
        }
    }
}