using System;
namespace SearchFilterService
{   
    //Builder
    public interface ISearchQueryBuilder
    {
        void SetDate(string date);
        void SetLocation(string location);
        void SetArtist(string artist);
        SearchQuery Build();
    }
}