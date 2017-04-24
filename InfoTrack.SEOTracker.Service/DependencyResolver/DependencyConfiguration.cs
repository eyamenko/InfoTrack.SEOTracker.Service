using System;
using Microsoft.Extensions.DependencyInjection;
using InfoTrack.SEOTracker.Service.Contracts.ApiClients;
using InfoTrack.SEOTracker.Service.ApiClients;
using InfoTrack.SEOTracker.Service.Configuration;
using InfoTrack.SEOTracker.Service.Contracts.Services;
using InfoTrack.SEOTracker.Service.Services;
using InfoTrack.SEOTracker.Service.Contracts;

namespace InfoTrack.SEOTracker.Service.DependencyResolver
{
    public static class DependencyConfiguration
    {
        private static readonly object _lock = new object();
        private static readonly AppConfiguration Configuration = AppConfiguration.Instance;

        private static IServiceProvider _instance;

        private static IServiceProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = Build();
                        }
                    }
                }

                return _instance;
            }
        }

        private static IServiceProvider Build()
        {
            var collection = new ServiceCollection();

            #region Worker

            collection.AddSingleton<IWorker, Worker>();

            #endregion

            #region Api Clients

            collection.AddSingleton<ISEOTrackerApiClient, SEOTrackerApiClient>(_ => new SEOTrackerApiClient(Configuration.Urls.SEOTrackerApi));
            collection.AddSingleton<ISearchApiClient, GoogleSearchApiClient>(_ => new GoogleSearchApiClient(Configuration.Urls.GoogleApi, Configuration.Resources.SearchApiKey, Configuration.Resources.SearchEngineId));

            #endregion

            #region Sevices

            collection.AddSingleton<ISearchService, SearchService>();

            #endregion

            return collection.BuildServiceProvider();
        }

        public static T Resolve<T>()
        {
            return Instance.GetService<T>();
        }
    }
}