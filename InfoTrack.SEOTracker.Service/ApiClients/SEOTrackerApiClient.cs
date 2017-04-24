using InfoTrack.SEOTracker.Service.ApiClients.Primitives;
using System.Threading.Tasks;
using InfoTrack.SEOTracker.Service.Models;
using System.Collections.Generic;
using InfoTrack.SEOTracker.Service.Utils.Extensions;
using InfoTrack.SEOTracker.Service.Contracts.ApiClients;

namespace InfoTrack.SEOTracker.Service.ApiClients
{
    public class SEOTrackerApiClient : BaseApiClient, ISEOTrackerApiClient
    {
        public SEOTrackerApiClient(string endpoint) : base(endpoint) { }

        public Task<bool> AddPositions(IEnumerable<SearchPosition> positions)
        {
            return GetSafeResponseAsync(c => c.PostAsync("v1/searchresults", positions.ToJsonContent()));
        }
    }
}