namespace SearchFilterService
{
    public class SearchDirector
    {
        private ISearchQueryBuilder _builder;

        public void SetBuilder(ISearchQueryBuilder builder)
        {
            _builder = builder;
        }

        [ExecutionTimeAspect]
        public SearchQuery ConstructSearchQuery(string date, string location, string artist)
        {
            _builder.SetDate(date);
            _builder.SetLocation(location);
            _builder.SetArtist(artist);
            return _builder.Build();
        }
    }
}
