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
                new Event {Name = "Adele Live", Date = DateTime.Now, Location = "London", Genre = "Pop", Type = "Concert", MainArtist = "Adele", Description = "Adele's live concert in London", Image = "adele.jpg"},
                new Event {Name = "Rock in Rio", Date = DateTime.Now, Location = "Rio de Janeiro", Genre = "Rock", Type = "Festival", MainArtist = "Foo Fighters", Description = "Rock in Rio festival in Rio de Janeiro", Image = "rockinrio.jpg"},
                new Event {Name = "Tomorrowland", Date = DateTime.Now, Location = "Boom", Genre = "Electronic", Type = "Festival", MainArtist = "David Guetta", Description = "Tomorrowland festival in Boom", Image = "tomorrowland.jpg"},
                new Event {Name = "Theatre Play", Date = DateTime.Now, Location = "New York", Genre = "Drama", Type = "Theatre", Theme = "Drama", Description = "Theatre play in New York", Image = "theatre.jpg"},
                new Event {Name = "Art Exhibition", Date = DateTime.Now, Location = "Paris", Genre = "Art", Type = "Exhibition", Theme = "Art", Description = "Art exhibition in Paris", Image = "art.jpg"},
                new Event {Name = "Jazz Night", Date = DateTime.Now, Location = "New Orleans", Genre = "Jazz", Type = "Concert", MainArtist = "Louis Armstrong", Description = "Jazz night in New Orleans", Image = "jazz.jpg"}                
            };

            if (preferences == null)
                throw new ArgumentNullException(nameof(preferences), "Preferences cannot be null.");

            if (preferences.FavoriteGenres == null || preferences.FavoriteGenres.Count == 0 || preferences.PreferredLocations == null || preferences.PreferredLocations.Count == 0)
                return new List<Event>();

            var userEventHistory = preferences.EventHistory
                .SelectMany(history => history.Events)
                .ToList();

            return availableEvents.FindAll(e =>
            {
                if (e == null || string.IsNullOrEmpty(e.Genre) || string.IsNullOrEmpty(e.Location))
                    return false;

                bool matchesGenre = preferences.FavoriteGenres.Contains(e.Genre);
                bool matchesLocation = preferences.PreferredLocations.Contains(e.Location, StringComparer.OrdinalIgnoreCase);

                bool isInHistory = userEventHistory.Any(uh => uh.Event.Name == e.Name);
                bool wasPreferred = userEventHistory.Any(uh => uh.Event.Name == e.Name && uh.IsPreferred);

                return matchesGenre && matchesLocation && (!isInHistory || wasPreferred);
            });
        }
    }
}
