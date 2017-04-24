using System.Collections.Generic;
using System.Linq;

namespace InfoTrack.SEOTracker.Service.Utils.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }
    }
}