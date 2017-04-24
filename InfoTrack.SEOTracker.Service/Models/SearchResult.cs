using System;
using System.Collections.Generic;

namespace InfoTrack.SEOTracker.Service.Models
{
    public class SearchResult
    {
        public SearchResult()
        {
            this.Items = new List<SearchItem>();
        }

        public IEnumerable<SearchItem> Items { get; set; }
    }
}