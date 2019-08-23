using System.Collections.Generic;
using PollyPOC.Core.Models;
using PollyPOC.Models;

namespace PollyPOC.Core.Services
{
    public interface IWeatherService
    {
        Forecast GetForecast(bool shouldFail);
        List<DailyForecast> GetDailyForecast();
    }
}