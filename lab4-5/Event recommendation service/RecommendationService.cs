using System.Collections.Generic;

namespace EventRecommendationService
{
    public class RecommendationService
    {
        private IRecommendationEngine _recommendationEngine;

        public RecommendationService(IRecommendationEngine recommendationEngine)
        {
            _recommendationEngine = recommendationEngine;
        }

        public void SetRecommendationEngine(IRecommendationEngine recommendationEngine)
        {
            _recommendationEngine = recommendationEngine;
        }

        public List<Event> GetRecommendations(UserPreferences userPreferences)
        {
            var clonedPreferences = (UserPreferences)userPreferences.Clone();
            return _recommendationEngine.RecommendEvents(clonedPreferences);
        }
    }
}
