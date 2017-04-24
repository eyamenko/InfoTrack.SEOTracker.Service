using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfoTrack.SEOTracker.Service.Contracts.ApiClients;
using InfoTrack.SEOTracker.Service.Contracts.Services;
using InfoTrack.SEOTracker.Service.Models;
using InfoTrack.SEOTracker.Service.Utils.Extensions;

namespace InfoTrack.SEOTracker.Service.Services
{
    public class SearchService : ISearchService
    {
        private const int MAX_RESULTS = 100;
        private const int NUMBER_OF_ITEMS = 10;

        private readonly ISearchApiClient _searchApiClient;
        private readonly ISEOTrackerApiClient _seoTrackerApiClient;

        public SearchService(ISearchApiClient searchApiClient, ISEOTrackerApiClient seoTrackerApiClient)
        {
            _searchApiClient = searchApiClient;
            _seoTrackerApiClient = seoTrackerApiClient;
        }

        public async Task<IEnumerable<SearchPosition>> GetPositions(string url, string query)
        {
            var searchPositions = new List<SearchPosition>();

            foreach (var index in Enumerable.Range(0, MAX_RESULTS / NUMBER_OF_ITEMS))
            {
                var searchResult = await _searchApiClient.Search(query, index * NUMBER_OF_ITEMS + 1, NUMBER_OF_ITEMS);

                if (searchResult != null)
                    searchPositions.AddRange(Map(searchResult, index, url));
            }

            return searchPositions;
        }

        public async Task SavePositions(IEnumerable<SearchPosition> positions)
        {
            if (positions.IsEmpty())
                throw new ArgumentException("Collection is empty", nameof(positions));

            if (!await _seoTrackerApiClient.AddPositions(positions))
                throw new Exception("Couldn't save positions");
        }

        private static IEnumerable<SearchPosition> Map(SearchResult searchResult, int index, string url)
        {
            return searchResult
                .Items
                .Select((i, idx) => new SearchPosition(i.Link, index * NUMBER_OF_ITEMS + idx + 1))
                .Where(sp => sp.Url.Contains(url));
        }
    }
}
