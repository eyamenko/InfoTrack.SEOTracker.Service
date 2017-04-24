using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace InfoTrack.SEOTracker.Service.Utils.Extensions
{
    public static class ObjectExtensions
    {
        public static HttpContent ToJsonContent(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}