using System;
using System.Collections.Generic;
using System.Linq;

// Strategy implementation
namespace EventRecommendationService
{
    public class RuleBasedEngine : IRecommendationEngine
    {

        public RuleBasedEngine()
        {
           
        }

        public List<Event> RecommendEvents(UserPreferences preferences)
        {
            var availableEvents = new List<Event>
            {
                new ConcertEvent { Name = "Rock Fest", Genre = "Rock", Date = DateTime.Now.AddDays(10), Location = "New York", MainArtist = "Band A" },
                new ConcertEvent { Name = "Pop Gala", Genre = "Pop", Date = DateTime.Now.AddDays(15), Location = "Los Angeles", MainArtist = "Artist B" },
                new ExhibitionEvent { Name = "Art Expo", Genre = "Art", Date = DateTime.Now.AddDays(5), Location = "Paris", Theme = "Modern Art" },
                new ConcertEvent { Name = "Jazz Night", Genre = "Jazz", Date = DateTime.Now.AddDays(20), Location = "Chicago", MainArtist = "Artist C" }
            };

            if (preferences == null)
                throw new ArgumentNullException(nameof(preferences), "Preferences cannot be null.");

            if (preferences.FavoriteGenres == null || preferences.FavoriteGenres.Count == 0 || preferences.PreferredLocations == null || preferences.PreferredLocations.Count == 0)
                return new List<Event>();

            return availableEvents.FindAll(e =>
            {
                if (e == null || string.IsNullOrEmpty(e.Genre) || string.IsNullOrEmpty(e.Location))
                    return false;

                bool matchesGenre = preferences.FavoriteGenres.Contains(e.Genre);

                bool matchesLocation = preferences.PreferredLocations.Contains(e.Location, StringComparer.OrdinalIgnoreCase);

                return matchesGenre && matchesLocation;
            });
        }
    }
}
