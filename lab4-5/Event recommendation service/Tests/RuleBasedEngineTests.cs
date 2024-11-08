using System;
using System.Collections.Generic;

namespace EventRecommendationService.Tests
{
    public class RuleBasedEngineTests
    {
        public static void Main()
        {
            RecommendEvents_ShouldReturnEventsMatchingUserGenreAndLocationPreferences();
            RecommendEvents_ShouldReturnEventsWhenLocationIsNullOrEmpty();
          
        }

        public static void RecommendEvents_ShouldReturnEventsMatchingUserGenreAndLocationPreferences()
        {
            var preferences = new UserPreferences
            {
                FavoriteGenres = new List<string> { "Rock", "Jazz" },
                PreferredLocation = "New York"
            };

            var events = new List<Event>
            {
                new ConcertEvent { Genre = "Rock", Location = "New York", Name = "Rock Concert", Date = DateTime.Now },
                new ConcertEvent { Genre = "Rock", Location = "Los Angeles", Name = "Rock Concert LA", Date = DateTime.Now },
                new ExhibitionEvent { Genre = "Jazz", Location = "New York", Name = "Jazz Exhibition", Date = DateTime.Now },
                new ExhibitionEvent { Genre = "Jazz", Location = "Chicago", Name = "Jazz Exhibition Chicago", Date = DateTime.Now },
                new ConcertEvent { Genre = "Pop", Location = "New York", Name = "Pop Concert", Date = DateTime.Now }
            };

            var ruleBasedEngine = new RuleBasedEngine(events);

            var recommendedEvents = ruleBasedEngine.RecommendEvents(preferences);

            bool countIsCorrect = recommendedEvents.Count == 2;
            bool containsNewYorkRockConcert = recommendedEvents.Exists(e => e is ConcertEvent concert && concert.Genre == "Rock" && concert.Location == "New York");
            bool containsNewYorkJazzExhibition = recommendedEvents.Exists(e => e is ExhibitionEvent exhibition && exhibition.Genre == "Jazz" && exhibition.Location == "New York");

            if (countIsCorrect && containsNewYorkRockConcert && containsNewYorkJazzExhibition)
            {
                Console.WriteLine("RecommendEvents_ShouldReturnEventsMatchingUserGenreAndLocationPreferences: Passed");
            }
            else
            {
                Console.WriteLine("RecommendEvents_ShouldReturnEventsMatchingUserGenreAndLocationPreferences: Failed");
                if (!countIsCorrect)
                    Console.WriteLine($"Expected 2 events, but got {recommendedEvents.Count}.");
                if (!containsNewYorkRockConcert)
                    Console.WriteLine("Expected to find Rock concert in New York, but it was missing.");
                if (!containsNewYorkJazzExhibition)
                    Console.WriteLine("Expected to find Jazz exhibition in New York, but it was missing.");
            }
        }
        
        public static void RecommendEvents_ShouldReturnEventsWhenLocationIsNullOrEmpty()
        {
            var preferences = new UserPreferences
            {
                FavoriteGenres = new List<string> { "Rock", "Jazz" },
                PreferredLocation = ""
            };
        
            var events = new List<Event>
            {
                new ConcertEvent { Genre = "Rock", Location = "New York", Name = "Rock Concert", Date = DateTime.Now },
                new ExhibitionEvent { Genre = "Jazz", Location = "Chicago", Name = "Jazz Exhibition", Date = DateTime.Now }
            };
        
            var ruleBasedEngine = new RuleBasedEngine(events);
            var recommendedEvents = ruleBasedEngine.RecommendEvents(preferences);
        
            bool countIsCorrect = recommendedEvents.Count == 2;
            bool containsRockConcert = recommendedEvents.Exists(e => e is ConcertEvent concert && concert.Genre == "Rock");
            bool containsJazzExhibition = recommendedEvents.Exists(e => e is ExhibitionEvent exhibition && exhibition.Genre == "Jazz");
        
            if (countIsCorrect && containsRockConcert && containsJazzExhibition)
            {
                Console.WriteLine("RecommendEvents_ShouldReturnEventsWhenLocationIsNullOrEmpty: Passed");
            }
            else
            {
                Console.WriteLine("RecommendEvents_ShouldReturnEventsWhenLocationIsNullOrEmpty: Failed");
            }
        }
        

    }
    
}
