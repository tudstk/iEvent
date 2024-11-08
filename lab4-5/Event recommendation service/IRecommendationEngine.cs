using System.Collections.Generic;

//Strategy interface
namespace EventRecommendationService
{
    public interface IRecommendationEngine
    {
        List<Event> RecommendEvents(UserPreferences preferences);
    }
}
