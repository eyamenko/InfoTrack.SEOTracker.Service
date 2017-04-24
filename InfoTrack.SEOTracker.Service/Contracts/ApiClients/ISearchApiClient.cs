using System;
using System.Threading.Tasks;
using InfoTrack.SEOTracker.Service.Models;

namespace InfoTrack.SEOTracker.Service.Contracts.ApiClients
{
    public interface ISearchApiClient
    {
        Task<SearchResult> Search(string query, int start, int numberOfItems);
    }
}