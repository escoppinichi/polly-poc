using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

public class WeatherService : IWeatherService
{
    private int currentTry = 0;
    private static readonly HttpClient client = new HttpClient();

    public WeatherService()
    {
        client.DefaultRequestHeaders.Accept.Clear();
    }
    public Forecast GetForecast(bool shouldFail)
    {
        // This uses OpenWeatherMap API
        // Get an API Key in their website to test this service.

        // This variable is used to simulate an error. Given N tries, the service will 'successfully' retrieve the data.
        // Otherwise return an exception.
        Console.WriteLine("Getting forecast from WeatherService");

        if (shouldFail)
            if (++currentTry % 5 != 0)
            {
                Console.WriteLine("Throwing exception for WeatherService.");
                throw new Exception("WeatherService has returned an error.");
            }


        var data = client.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?q=Guadalajara,MX&units=metric&appid=API_KEY").Result;
        dynamic results = JObject.Parse(data);

        var forecast = new Forecast()
        {
            Provider = "OpenWeatherMap",
            City = results.name,
            MinTemp = results.main.temp_min,
            MaxTemp = results.main.temp_max,
            Conditions = results.weather[0].description
        };

        return forecast;
    }


    public List<DailyForecast> GetDailyForecast()
    {
        var result = new List<DailyForecast>();

        return result;
    }
}