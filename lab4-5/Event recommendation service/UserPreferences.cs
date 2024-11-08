using System;
using System.Collections.Generic;

//Prototype
namespace EventRecommendationService
{
    public class UserPreferences : ICloneable
    {
        public List<string> FavoriteGenres { get; set; }
        public List<string> FavoriteArtists { get; set; }
        public string PreferredLocation { get; set; }

        public object Clone()
        {
            return new UserPreferences
            {
                FavoriteGenres = new List<string>(this.FavoriteGenres),
                FavoriteArtists = new List<string>(this.FavoriteArtists),
                PreferredLocation = this.PreferredLocation
            };
        }
    }
}