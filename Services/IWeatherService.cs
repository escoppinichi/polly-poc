using System.Collections.Generic;

public interface IWeatherService {
    Forecast GetForecast(bool shouldFail);
    List<DailyForecast> GetDailyForecast();
}