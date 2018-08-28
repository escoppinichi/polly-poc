using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace PollyPOC.Controllers
{

    [Route("api/weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {

        private IWeatherService _weatherService = new WeatherService();
        private IWeatherService _fallbackWeatherService = new FallbackWeatherService();

        [Route("retry")]
        public ActionResult<Forecast> RetryPolicyExample()
        {
            /*
                This will try to execute the service, and retry inmediately after a failed call.                
             */
            var result = RetryPolicy.GetRetryPolicy()
                                    .Execute(() => _weatherService.GetForecast(shouldFail: true));
            return result;
        }

        [Route("wait")]
        public ActionResult<Forecast> RetryAndWaitPolicyExample()
        {
            /*
                This will try to execute the service, and retry every three seconds after each failure. On each failure,
                the policy adds additional three seconds to the wait time.                
             */
            var result = RetryPolicy.GetRetryAndWaitPolicy()
                                    .Execute(() => _weatherService.GetForecast(shouldFail: true));
            return result;
        }

        [Route("fallback")]
        public ActionResult<Forecast> FallbackPolicyExample()
        {
            /*
                This example will try to call the default service, if that fails it will call an alternate service that provides
                the same information.
             */
            var result = FallbackPolicy.GetFallbackPolicy(_fallbackWeatherService.GetForecast)
                                        .Execute(() => _weatherService.GetForecast(shouldFail: true));

            return result;
        }

        [Route("wrap")]
        public ActionResult<Forecast> WrapPoliciesExample()
        {

            var retryPolicy = RetryPolicy.GetRetryPolicy(retryCount: 1);
            var retryAndWaitPolicy = RetryPolicy.GetRetryAndWaitPolicy(retryCount: 1);
            var fallbackPolicy = FallbackPolicy.GetFallbackPolicy(_fallbackWeatherService.GetForecast);

            // Policy Wrap can combine multiple policies.
            // In this example, try retrying immediately first, then wait N seconds to retry, and if that fails, use a fallback.

            // Policy run order is from the latest wrapped to the first wrapped.
            // This will execute retry, then retryAndWait and finally fallback.
            var policyWrap = fallbackPolicy.Wrap(retryAndWaitPolicy).Wrap(retryPolicy);

            var result = policyWrap.Execute(() => _weatherService.GetForecast(shouldFail: true));

            return result;
        }

        [Route("circuitbreaker")]
        public ActionResult<Forecast> CircuitBreakerExample()
        {
            /*
                This example will trigger a circuit breaker. It will make WeatherService unavailable for a period of time after N failures.
                As an additional example, we will combine this circuit breaker so all subssecuent calls will fallback fo FallbackWeatherService
                while the circuit is broken.
             */

            var fallbackPolicy = FallbackPolicy.GetFallbackPolicy(_fallbackWeatherService.GetForecast);

            var policyWrap = fallbackPolicy.Wrap(CircuitBreakerPolicy.circuitBreakerPolicy);

            var result = policyWrap.Execute(() => _weatherService.GetForecast(shouldFail: true));

            return result;
        }
    }

}