using System;
namespace SearchFilterService
{
    public interface ISearchQueryBuilder
    {
        void SetDate(string date);
        void SetLocation(string location);
        void SetArtist(string artist);
        SearchQuery Build();
    }
}