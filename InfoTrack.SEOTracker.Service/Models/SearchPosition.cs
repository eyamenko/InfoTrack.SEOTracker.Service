using System;

namespace InfoTrack.SEOTracker.Service.Models
{
    public class SearchPosition
    {
        public SearchPosition(string url, int index)
        {
            this.Url = url;
            this.Index = index;
        }

        public int Index { get; set; }
        public string Url { get; set; }
    }
}