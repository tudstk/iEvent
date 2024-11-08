using System;
namespace SearchFilterService
{
    public class ConcreteSearchQueryBuilder : ISearchQueryBuilder
    {
        private readonly SearchQuery _searchQuery = new SearchQuery();

    
        public ConcreteSearchQueryBuilder SetDate(string date)
        {
            _searchQuery.Date = date;
            return this
        }

        public ConcreteSearchQueryBuilder SetLocation(string location)
        {
            _searchQuery.Location = location;
            return this;
        }

        public ConcreteSearchQueryBuilder SetArtist(string artist)
        {
            _searchQuery.Artist = artist;
            return this;
        }

        public SearchQuery Build()
        {
            return _searchQuery;
        }
    }
}
