using System;
using System.Collections.Generic;

namespace EventRecommendationService
{
    public class UserPreferences : ICloneable
    {
        public List<string> FavoriteGenres { get; set; }
        public List<string> FavoriteArtists { get; set; }
        public List<string> PreferredLocations { get; set; }

        public List<UserEventHistory> EventHistory { get; set; }

        public UserPreferences()
        {
            FavoriteGenres = new List<string>();
            FavoriteArtists = new List<string>();
            PreferredLocations = new List<string>();
            EventHistory = new List<UserEventHistory>();
        }

        public object Clone()
        {
            return new UserPreferences
            {
                FavoriteGenres = new List<string>(this.FavoriteGenres),
                FavoriteArtists = new List<string>(this.FavoriteArtists),
                PreferredLocations = new List<string>(this.PreferredLocations),
                EventHistory = new List<UserEventHistory>(this.EventHistory)
            };
        }

        public void AddEventToHistory(string eventName, string genre, string location, DateTime eventDate, bool isPreferred)
        {
            EventHistory.Add(new UserEventHistory
            {
                EventName = eventName,
                Genre = genre,
                Location = location,
                EventDate = eventDate,
                IsPreferred = isPreferred
            });
        }
    }
    public class UserEventHistory
    {
        public string EventName { get; set; }
        public string Genre { get; set; }
        public string Location { get; set; }
        public DateTime EventDate { get; set; }
        public bool IsPreferred { get; set; }  
    }
}
