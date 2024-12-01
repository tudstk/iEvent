using System;
using System.Collections.Generic;

namespace EventRecommendationService
{
    // Event interface
    public interface Event
    {
        string Name { get; set; }
        DateTime Date { get; set; }
        string Location { get; set; }
        string Genre { get; set; }
    }

    // Concert Event class
    public class ConcertEvent : Event
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string MainArtist { get; set; }
        public string Genre { get; set; }
    }

    // Exhibition Event class
    public class ExhibitionEvent : Event
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Theme { get; set; }
        public string Genre { get; set; }
    }
}
