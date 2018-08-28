using System;
using System.Diagnostics;
using Polly;

public class RetryPolicy
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
                    retryCount: retryCount,
                    onRetry: (e, i) =>
                    {
                        Console.WriteLine($"Service has failed ({e.ToString()}). Trying attempt #{i}");
                    }
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
            retryCount: retryCount,
            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(3 * retryAttempt),
            onRetry: (e, i) =>
            {
                Console.WriteLine($"Service has failed ({e.ToString()}). Trying again in {i.TotalSeconds} seconds.");
            }
        );
    }
}