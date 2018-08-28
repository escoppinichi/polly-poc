using Polly;
using System;
using System.Collections.Generic;

public class FallbackPolicy
{
    public static Policy<Forecast> GetFallbackPolicy(Func<bool, Forecast> fallbackHandler)
    {
        /*
            Specify a substitute func, calling an action (eg for logging) if the fallback is invoked.

            https://github.com/App-vNext/Polly#fallback
        */

        return Policy<Forecast>
            .Handle<Exception>()
            .Fallback<Forecast>(() => fallbackHandler(false), (e) =>
            {
                Console.WriteLine("Calling fallback service...");
            });
    }
}