using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfoTrack.SEOTracker.Service.Models;

namespace InfoTrack.SEOTracker.Service.Contracts.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<SearchPosition>> GetPositions(string url, string query);
        Task SavePositions(IEnumerable<SearchPosition> positions);
    }
}