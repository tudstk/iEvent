using System.Collections.Generic;
using System;

//Strategy implementation
namespace EventRecommendationService
{
    public class RuleBasedEngine : IRecommendationEngine
    {
        private readonly List<Event> _events;

        public RuleBasedEngine(List<Event> events)
        {
            _events = events;
        }

        public List<Event> RecommendEvents(UserPreferences preferences){
         if (preferences == null || preferences.FavoriteGenres == null || preferences.FavoriteGenres.Count == 0)
                return new List<Event>();  

            return _events.FindAll(e =>
            {
                if (e == null || string.IsNullOrEmpty(e.Genre) || string.IsNullOrEmpty(e.Location))
                    return false;

                bool matchesGenre = preferences.FavoriteGenres.Contains(e.Genre);
                bool matchesLocation = string.IsNullOrEmpty(preferences.PreferredLocation) || e.Location.Equals(preferences.PreferredLocation, StringComparison.OrdinalIgnoreCase);

                return matchesGenre && matchesLocation;
            });
        }
    }
}
