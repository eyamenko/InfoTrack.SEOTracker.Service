using Microsoft.Extensions.Configuration;
using System.IO;
using System;

namespace InfoTrack.SEOTracker.Service.Configuration
{
    public class AppConfiguration
    {
        private static readonly object _lock = new object();

        private static AppConfiguration _instance;

        private static AppConfiguration Build()
        {
            var environment = Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "DEVELOPMENT";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{environment.ToLowerInvariant()}.json", true);

            var configuration = builder.Build();

            var urls = new Urls();
            var resources = new Resources();

            configuration.GetSection("urls").Bind(urls);
            configuration.GetSection("resources").Bind(resources);

            return new AppConfiguration
            {
                Resources = resources,
                Urls = urls
            };
        }

        public static AppConfiguration Instance
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

        private AppConfiguration() { }

        public Urls Urls
        {
            get;
            set;
        }

        public Resources Resources
        {
            get;
            set;
        }
    }
}