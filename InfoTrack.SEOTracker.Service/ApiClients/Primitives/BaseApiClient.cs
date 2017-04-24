using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InfoTrack.SEOTracker.Service.ApiClients.Primitives
{
    public abstract class BaseApiClient
    {
        private readonly HttpClient _client;

        protected BaseApiClient(string endpoint)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(endpoint)
            };
        }

        protected async Task<T> GetSafeResponseAsync<T>(Func<HttpClient, Task<HttpResponseMessage>> func)
        {
            try
            {
                using (var response = await func(_client))
                {
                    response.EnsureSuccessStatusCode();

                    using (var content = response.Content)
                    {
                        var json = await content.ReadAsStringAsync();

                        return JsonConvert.DeserializeObject<T>(json);
                    }
                }
            }
            catch
            {
                return default(T);
            }
        }

        protected async Task<bool> GetSafeResponseAsync(Func<HttpClient, Task<HttpResponseMessage>> func)
        {
            try
            {
                using (var response = await func(_client))
                {
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }

        ~BaseApiClient()
        {
            _client.Dispose();
        }
    }
}