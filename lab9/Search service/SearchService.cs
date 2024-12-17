using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EventRecommendationService;

namespace EventSearch
{
    public class SearchService
    {
        private readonly List<Event> _allEvents;

        public SearchService(string filePath)
        {
            _allEvents = LoadEventsFromFile(filePath);
        }

        private List<Event> LoadEventsFromFile(string filePath)
        {
            var events = new List<Event>();

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"The file {filePath} does not exist.");

            var lines = File.ReadAllLines(filePath).Skip(1);

            foreach (var line in lines)
            {
                var values = line.Split(',');

                events.Add(new Event
                {
                    Name = values[0],
                    Date = DateTime.Parse(values[1]),
                    Location = values[2],
                    Genre = values[3],
                    Type = values[4],
                    MainArtist = values[5],
                    Theme = values[6],
                    Description = values[7],
                    Image = values[8]
                });
            }

            return events;
        }

        public List<Event> SearchEvents(EventSearchCriteria criteria)
        {
            var filteredEvents = _allEvents.AsEnumerable();

            if (!string.IsNullOrEmpty(criteria.Genre))
                filteredEvents = filteredEvents.Where(e => e.Genre.Equals(criteria.Genre, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(criteria.Location))
                filteredEvents = filteredEvents.Where(e => e.Location.Equals(criteria.Location, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(criteria.Artist))
                filteredEvents = filteredEvents.Where(e => e.MainArtist?.Equals(criteria.Artist, StringComparison.OrdinalIgnoreCase) == true);

            if (!string.IsNullOrEmpty(criteria.Type))
                filteredEvents = filteredEvents.Where(e => e.Type.Equals(criteria.Type, StringComparison.OrdinalIgnoreCase));

            return filteredEvents.ToList();
        }
    }
}
