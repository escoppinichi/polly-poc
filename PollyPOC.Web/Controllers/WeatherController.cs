using Microsoft.AspNetCore.Mvc;
using PollyPOC.Core;
using PollyPOC.Models;

namespace PollyPOC.Web.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        [Route("retry")]
        public ActionResult<Forecast> RetryPolicyExample()
        {
            return Examples.RetryPolicyExample();
        }

        [Route("wait")]
        public ActionResult<Forecast> RetryAndWaitPolicyExample()
        {
            return Examples.RetryAndWaitPolicyExample();
        }

        [Route("fallback")]
        public ActionResult<Forecast> FallbackPolicyExample()
        {
            return Examples.FallbackPolicyExample();
        }

        [Route("wrap")]
        public ActionResult<Forecast> WrapPoliciesExample()
        {
            return Examples.WrapPoliciesExample();
        }

        [Route("circuitbreaker")]
        public ActionResult<Forecast> CircuitBreakerExample()
        {
            return Examples.CircuitBreakerExample();
        }

        [Route("cache")]
        public ActionResult<Forecast> CacheExample()
        {
            return Examples.CacheExample();
        }
    }
}