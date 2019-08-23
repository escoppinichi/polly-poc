using System;
using System.Drawing;
using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Caching.Memory;
using PollyPOC.Models;
using Console = Colorful.Console;

namespace PollyPOC.Core.Policies
{
    public static class CachePolicy
    {
        private static readonly MemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

        public static Policy<Forecast> GetCachePolicy(int secondsToExpire)
        {
            var memoryCacheProvider = new MemoryCacheProvider(memoryCache);

            var cachePolicy = Policy.Cache<Forecast>(cacheProvider: memoryCacheProvider,
                ttl: TimeSpan.FromSeconds(secondsToExpire),
                (context, s) => { Console.WriteLine("Getting forecast from cache.", Color.Green); }, (context, s) =>
                {
                    Console.WriteLine(
                        "Trying to get data from cache but data has expired or not found... not returning data from cache.",
                        Color.Yellow);
                }, (context, s) => { Console.WriteLine("Saving forecast to cache...", Color.Yellow); },
                (context, s, e) => { },
                (context, s, e) => { });

            return cachePolicy;
        }
    }
}