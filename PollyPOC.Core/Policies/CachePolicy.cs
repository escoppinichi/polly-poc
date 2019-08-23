using System;
using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Caching.Memory;

namespace PollyPOC.Core.Policies
{
    public static class CachePolicy
    {
        private static readonly MemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

        public static Policy GetCachePolicy(int minutesToExpire)
        {
            var memoryCacheProvider = new MemoryCacheProvider(memoryCache);

            return Policy.Cache(memoryCacheProvider, TimeSpan.FromMinutes(minutesToExpire));
        }
    }
}