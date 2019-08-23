using Polly;
using PollyPOC.Core.Policies;
using PollyPOC.Core.Services;
using PollyPOC.Core.Services.Implementation;
using PollyPOC.Models;

namespace PollyPOC.Console
{
    class Program
    {
        private static readonly IWeatherService _fallbackWeatherService = new FallbackWeatherService();
        private static readonly IWeatherService _weatherService = new WeatherService();

        static void Main(string[] args)
        {
            RetryPolicyExample();
            RetryAndWaitPolicyExample();
            FallbackPolicyExample();
            WrapPoliciesExample();

            for (var i = 0; i < 5; i++)
                CircuitBreakerExample();

            for (var i = 0; i < 5; i++)
                CacheExample();
        }

        static Forecast RetryPolicyExample()
        {
            /*
                This will try to execute the service, and retry immediately after a failed call.
             */
            var result = RetryPolicy.GetRetryPolicy()
                .Execute(() => _weatherService.GetForecast(true));
            return result;
        }

        static Forecast RetryAndWaitPolicyExample()
        {
            /*
                This will try to execute the service, and retry every three seconds after each failure. On each failure,
                the policy adds additional three seconds to the wait time.
             */
            var result = RetryPolicy.GetRetryAndWaitPolicy()
                .Execute(() => _weatherService.GetForecast(true));
            return result;
        }

        static Forecast FallbackPolicyExample()
        {
            /*
                This example will try to call the default service, if that fails it will call an alternate service that provides
                the same information.
             */
            var result = FallbackPolicy.GetFallbackPolicy(_fallbackWeatherService.GetForecast)
                .Execute(() => _weatherService.GetForecast(true));

            return result;
        }

        static Forecast WrapPoliciesExample()
        {
            var retryPolicy = RetryPolicy.GetRetryPolicy(1);
            var retryAndWaitPolicy = RetryPolicy.GetRetryAndWaitPolicy(1);
            var fallbackPolicy = FallbackPolicy.GetFallbackPolicy(_fallbackWeatherService.GetForecast);

            // Policy Wrap can combine multiple policies.
            // In this example, try retrying immediately first, then wait N seconds to retry, and if that fails, use a fallback.

            // Policy run order is from the latest wrapped to the first wrapped.
            // This will execute retry, then retryAndWait and finally fallback.
            var policyWrap = fallbackPolicy.Wrap(retryAndWaitPolicy).Wrap(retryPolicy);

            var result = policyWrap.Execute(() => _weatherService.GetForecast(true));

            return result;
        }

        static Forecast CircuitBreakerExample()
        {
            /*
                This example will trigger a circuit breaker. It will make WeatherService unavailable for a period of time after N failures.
                As an additional example, we will combine this circuit breaker so all subsequent calls will fallback fo FallbackWeatherService
                while the circuit is broken.
             */

            var fallbackPolicy = FallbackPolicy.GetFallbackPolicy(_fallbackWeatherService.GetForecast);

            var policyWrap = fallbackPolicy.Wrap(CircuitBreakerPolicy.circuitBreakerPolicy);

            var result = policyWrap.Execute(() => _weatherService.GetForecast(true));

            return result;
        }

        static Forecast CacheExample()
        {
            /*
             * This example will call the default weather service and cache the response for five minutes.
             *
             * All subsequent calls to this endpoint will return the data from the cache if it is still valid.
             */

            var cachePolicy = CachePolicy.GetCachePolicy(5);

            var result = cachePolicy.Execute(context => _weatherService.GetForecast(false), new Context("WeatherKey"));

            return result;
        }
    }
}