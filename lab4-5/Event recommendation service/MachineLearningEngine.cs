using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace EventRecommendationService
{
    public class MachineLearningEngine : IRecommendationEngine
    {
        private readonly MLContext _mlContext;
        private ITransformer _model;
        private UserPreferences userPreferences;

        public MachineLearningEngine(UserPreferences preferences)
        {
            _mlContext = new MLContext();
            userPreferences = preferences;
            TrainModel(); 
        }

        private void TrainModel()
        {   
            if (userPreferences == null)
                throw new ArgumentNullException(nameof(userPreferences), "Preferences cannot be null.");

            var trainingData = new List<EventTrainingData>();

            foreach (var eventHistory in userPreferences.EventHistory)
            {
                trainingData.Add(new EventTrainingData
                {
                    Genre = eventHistory.Genre,
                    Location = eventHistory.Location,
                    IsPreferred = eventHistory.IsPreferred
                });
            }

            var trainDataView = _mlContext.Data.LoadFromEnumerable(trainingData);
            
            var pipeline = _mlContext.Transforms.Text.FeaturizeText("GenreFeatures", nameof(EventTrainingData.Genre))
                            .Append(_mlContext.Transforms.Text.FeaturizeText("LocationFeatures", nameof(EventTrainingData.Location)))
                            .Append(_mlContext.Transforms.Concatenate("Features", "GenreFeatures", "LocationFeatures"))
                            .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(
                            labelColumnName: nameof(EventTrainingData.IsPreferred), 
                            featureColumnName: "Features"));

            _model = pipeline.Fit(trainDataView);
        }

        public List<Event> RecommendEvents(UserPreferences preferences)
        {
            if (preferences == null)
                throw new ArgumentNullException(nameof(preferences), "Preferences cannot be null.");

            var availableEvents = new List<Event>
            {
                new ConcertEvent { Name = "Rock Fest", Genre = "Rock", Date = DateTime.Now.AddDays(10), Location = "New York", MainArtist = "Band A" },
                new ConcertEvent { Name = "Pop Gala", Genre = "Pop", Date = DateTime.Now.AddDays(15), Location = "Los Angeles", MainArtist = "Artist B" },
                new ExhibitionEvent { Name = "Art Expo", Genre = "Art", Date = DateTime.Now.AddDays(5), Location = "Paris", Theme = "Modern Art" },
                new ConcertEvent { Name = "Jazz Night", Genre = "Jazz", Date = DateTime.Now.AddDays(20), Location = "Chicago", MainArtist = "Artist C" }
            };

            var eventData = availableEvents.Select(evt => new EventInputData
            {
                Genre = evt.Genre,
                Location = evt.Location
            }).ToList();

            var eventDataView = _mlContext.Data.LoadFromEnumerable(eventData);

            var predictions = _model.Transform(eventDataView);
            var scoredResults = _mlContext.Data.CreateEnumerable<EventPrediction>(predictions, reuseRowObject: false).ToList();

            var recommendedEvents = new List<Event>();

            for (int i = 0; i < availableEvents.Count; i++)
            {
                if (scoredResults[i].Score > 0.5) 
                {
                    if (preferences.PreferredLocations != null && preferences.PreferredLocations.Contains(availableEvents[i].Location, StringComparer.OrdinalIgnoreCase))
                    {
                        recommendedEvents.Add(availableEvents[i]);
                    }
                }
            }

            return recommendedEvents;
        }
    }

    public class EventTrainingData
    {
        public string Genre { get; set; }
        public string Location { get; set; }
        public bool IsPreferred { get; set; }
    }

    public class EventInputData
    {
        public string Genre { get; set; }
        public string Location { get; set; }  
    }

    public class EventPrediction
    {
        [ColumnName("Score")]
        public float Score { get; set; }
    }
}
