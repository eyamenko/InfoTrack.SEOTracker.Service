using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfoTrack.SEOTracker.Service.Models;

namespace InfoTrack.SEOTracker.Service.Contracts.ApiClients
{
    public interface ISEOTrackerApiClient
    {
        Task<bool> AddPositions(IEnumerable<SearchPosition> positions);
    }
}