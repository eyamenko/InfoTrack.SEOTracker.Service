using System;
using InfoTrack.SEOTracker.Service.Contracts.Services;
using System.Threading.Tasks;
using Serilog;
using System.Linq;
using InfoTrack.SEOTracker.Service.Contracts;
using InfoTrack.SEOTracker.Service.Configuration;

namespace InfoTrack.SEOTracker.Service
{
    public class Worker : IWorker
    {
        private static readonly AppConfiguration Configuration = AppConfiguration.Instance;

        private readonly ISearchService _searchService;

        public Worker(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public async Task Run()
        {
            var positions = await _searchService.GetPositions(Configuration.Resources.SearchUrl, Configuration.Resources.SearchQuery);

            Log.Verbose(positions.Count() + " positions have been found");

            try
            {
                await _searchService.SavePositions(positions);

                Log.Verbose("Successfully saved positions");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}