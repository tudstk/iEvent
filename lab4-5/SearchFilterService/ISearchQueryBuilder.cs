using System;
namespace SearchFilterService
{   
    //Builder
    public interface ISearchQueryBuilder
    {
        ISearchQueryBuilder SetDate(string date);
        ISearchQueryBuilder SetLocation(string location);
        ISearchQueryBuilder SetArtist(string artist);
        SearchQuery Build();
    }
}