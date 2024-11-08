using System;

namespace SearchFilterService
{
    public class SearchQuery
    {
        public string Date { get; set; }
        public string Location { get; set; }
        public string Artist { get; set; }

        public override string ToString()
        {
            return $"Date: {Date}, Location: {Location}, Artist: {Artist}";
        }
    }
}
