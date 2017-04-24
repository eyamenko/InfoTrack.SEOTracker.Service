using System;
using InfoTrack.SEOTracker.Service.ApiClients.Primitives;
using System.Threading.Tasks;
using InfoTrack.SEOTracker.Service.Models;
using System.Net;
using Microsoft.AspNetCore.WebUtilities;
using InfoTrack.SEOTracker.Service.Contracts.ApiClients;
using System.Collections.Generic;

namespace InfoTrack.SEOTracker.Service.ApiClients
{
    public class GoogleSearchApiClient : BaseApiClient, ISearchApiClient
    {
        private readonly string _apiKey;
        private readonly string _searchEngineId;

        public GoogleSearchApiClient(string endpoint, string apiKey, string searchEngineId) : base(endpoint)
        {
            _apiKey = apiKey;
            _searchEngineId = searchEngineId;
        }

        public Task<SearchResult> Search(string query, int start, int numberOfItems)
        {
            var parameters = new[]
            {
                Tuple.Create("start", start.ToString()),
                Tuple.Create("num", numberOfItems.ToString()),
                Tuple.Create("q", query)
            };

            return GetSafeResponseAsync<SearchResult>(c => c.GetAsync(BuildUri("customsearch/v1", parameters)));
        }

        private string BuildUri(string resource, params Tuple<string, string>[] parameters)
        {
            var queryString = new Dictionary<string, string>
            {
                {"key", _apiKey},
                {"cx", _searchEngineId}
            };

            foreach (var parameter in parameters)
                queryString[parameter.Item1] = WebUtility.UrlEncode(parameter.Item2);

            var uri = QueryHelpers.AddQueryString(resource, queryString);

            return uri;
        }
    }
}