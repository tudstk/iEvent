using System.Collections.Generic;

namespace EventRecommendationService
{
    public class RecommendationService
    {
        private IRecommendationEngine _recommendationEngine;

        // Constructor cu dependency injection
        public RecommendationService(IRecommendationEngine recommendationEngine)
        {
            _recommendationEngine = recommendationEngine ?? 
                throw new ArgumentNullException(nameof(recommendationEngine), "Recommendation engine cannot be null");
        }

        [StrategyAspect]
        public void SetRecommendationEngine(IRecommendationEngine recommendationEngine)
        {
            _recommendationEngine = recommendationEngine ??
                throw new ArgumentNullException(nameof(recommendationEngine), "Recommendation engine cannot be null");
        }


        public List<Event> GetRecommendations(UserPreferences userPreferences)
        {
            if (userPreferences == null)
            {
                throw new ArgumentNullException(nameof(userPreferences), "User preferences cannot be null");
            }

            var clonedPreferences = (UserPreferences)userPreferences.Clone();

            return _recommendationEngine.RecommendEvents(clonedPreferences);
        }
    }
}
