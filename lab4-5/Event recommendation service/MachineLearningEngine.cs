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

                foreach (var userEvent in userPreferences.EventHistory)
                {
                    foreach(var evt in userEvent.Events)
                        trainingData.Add(new EventTrainingData
                        {
                            Genre = evt.Event.Genre,
                            Location = evt.Event.Location,
                            Type = evt.Event.Type,
                            IsPreferred = evt.IsPreferred
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
                new Event {Name = "Adele Live", Date = DateTime.Now, Location = "London", Genre = "Pop", Type = "Concert", MainArtist = "Adele", Description = "Adele's live concert in London", Image = "adele.jpg"},
                new Event {Name = "Rock in Rio", Date = DateTime.Now, Location = "Rio de Janeiro", Genre = "Rock", Type = "Festival", MainArtist = "Foo Fighters", Description = "Rock in Rio festival in Rio de Janeiro", Image = "rockinrio.jpg"},
                new Event {Name = "Tomorrowland", Date = DateTime.Now, Location = "Boom", Genre = "Electronic", Type = "Festival", MainArtist = "David Guetta", Description = "Tomorrowland festival in Boom", Image = "tomorrowland.jpg"},
                new Event {Name = "Theatre Play", Date = DateTime.Now, Location = "New York", Genre = "Drama", Type = "Theatre", Theme = "Drama", Description = "Theatre play in New York", Image = "theatre.jpg"},
                new Event {Name = "Art Exhibition", Date = DateTime.Now, Location = "Paris", Genre = "Art", Type = "Exhibition", Theme = "Art", Description = "Art exhibition in Paris", Image = "art.jpg"},
                new Event {Name = "Jazz Night", Date = DateTime.Now, Location = "New Orleans", Genre = "Jazz", Type = "Concert", MainArtist = "Louis Armstrong", Description = "Jazz night in New Orleans", Image = "jazz.jpg"}
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
        public string Type { get; set; }
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
