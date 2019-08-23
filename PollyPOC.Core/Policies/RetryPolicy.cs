using System;
using System.Drawing;
using Polly;
using Console = Colorful.Console;

namespace PollyPOC.Core.Policies
{
    public static class RetryPolicy
    {
        public static Policy GetRetryPolicy(int retryCount = 4)
        {
            /*
            Retry multiple times, calling an action on each retry 
            with the current exception and retry count

            https://github.com/App-vNext/Polly#retry
         */
            return Policy
                .Handle<Exception>()
                .Retry(
                    retryCount,
                    (e, i) => { Console.WriteLine($"Service has failed. Trying attempt #{i}", Color.Yellow); }
                );
        }

        public static Policy GetRetryAndWaitPolicy(int retryCount = 4)
        {
            /*       
            Retry, waiting a specified duration between each retry, 
            calling an action on each retry with the current exception
            and duration.

            https://github.com/App-vNext/Polly#wait-and-retry

         */
            return Policy
                .Handle<Exception>()
                .WaitAndRetry(
                    retryCount,
                    retryAttempt => TimeSpan.FromSeconds(3 * retryAttempt),
                    (e, i) =>
                    {
                        Console.WriteLine($"Service has failed. Trying again in {i.TotalSeconds} seconds.", Color.Yellow);
                    }
                );
        }
    }
}