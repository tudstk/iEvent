using System;
namespace SearchFilterService
{
    public class ConcreteSearchQueryBuilder : ISearchQueryBuilder
    {
        private readonly SearchQuery _searchQuery = new SearchQuery();

        public void SetDate(string date)
        {
            _searchQuery.Date = date;
        }

        public void SetLocation(string location)
        {
            _searchQuery.Location = location;
        }

        public void SetArtist(string artist)
        {
            _searchQuery.Artist = artist;
        }

        public SearchQuery Build()
        {
            return _searchQuery;
        }
    }
}
