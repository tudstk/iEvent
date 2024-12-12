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
            EventHistory = new List<UserEventHistory>(this.EventHistory.Select(eh => new UserEventHistory
            {
                Events = eh.Events.Select(e => new UserEvent
                {
                    Event = e.Event,
                    IsPreferred = e.IsPreferred
                }).ToList()
            }).ToList())
        };
    }
        
    }
        public class UserEventHistory
        {
            public List<UserEvent> Events { get; set; }

            public UserEventHistory()
            {
                Events = new List<UserEvent>();
            }
        }

        public class UserEvent
        {
            public Event Event { get; set; }
            public bool IsPreferred { get; set; }
        }

}
