using System;
using System.Drawing;
using Polly;
using PollyPOC.Models;
using Console = Colorful.Console;

namespace PollyPOC.Core.Policies
{
    public static class FallbackPolicy
    {
        public static Policy<Forecast> GetFallbackPolicy(Func<bool, Forecast> fallbackHandler)
        {
            /*
            Specify a substitute func, calling an action (eg for logging) if the fallback is invoked.

            https://github.com/App-vNext/Polly#fallback
        */

            return Policy<Forecast>
                .Handle<Exception>()
                .Fallback(() => fallbackHandler(false), e => { Console.WriteLine("Calling fallback service...", Color.Yellow); });
        }
    }
}