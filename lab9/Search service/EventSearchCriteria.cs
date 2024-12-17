namespace EventSearch
{
    public class EventSearchCriteria
    {
        public string Genre { get; private set; }
        public string Location { get; private set; }
        public string Artist { get; private set; }
        public string Type { get; private set; }
        public string Theme { get; private set; }
        public DateTime Date{ get; private set; }

        private EventSearchCriteria() { }

        public class Builder
        {
            private readonly EventSearchCriteria _criteria = new EventSearchCriteria();

            public Builder SetGenre(string genre)
            {
                _criteria.Genre = genre;
                return this;
            }

            public Builder SetLocation(string location)
            {
                _criteria.Location = location;
                return this;
            }

            public Builder SetArtist(string artist)
            {
                _criteria.Artist = artist;
                return this;
            }

            public Builder SetType(string type)
            {
                _criteria.Type = type;
                return this;
            }

            public Builder SetTheme(string theme)
            {
                _criteria.Theme = theme;
                return this;
            }

            public Builder SetDate(DateTime date)
            {
                _criteria.Date = date;
                return this;
            }

            public EventSearchCriteria Build()
            {
                return _criteria;
            }
        }
    }
}
