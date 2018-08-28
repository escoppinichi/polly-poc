using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;

public class FallbackWeatherService : IWeatherService
{
    // This variable is used to simulate an error. Given N tries, the service will 'successfully' retrieve the data.
    // Otherwise return an exception.
    private int currentTry = 0;
    private static readonly HttpClient client = new HttpClient();

    public FallbackWeatherService() {
        client.DefaultRequestHeaders.Accept.Clear();
    }
    public Forecast GetForecast(bool shouldFail)
    {
        // This uses Apixu API
        // Get an API Key in their website to test this service.

        // This variable is used to simulate an error. Given N tries, the service will 'successfully' retrieve the data.
        // Otherwise return an exception.
        Console.WriteLine("Getting forecast from FallbackWeatherService");

        if (shouldFail)
            if (++currentTry % 3 != 0)
            {
                Console.WriteLine("Throwing exception for FallbackWeatherService.");
                throw new Exception("FallbackWeatherService has returned an error.");
            }
     

        var data = client.GetStringAsync("https://api.apixu.com/v1/current.json?key=API_KEY&q=Guadalajara").Result;
        dynamic results = JObject.Parse(data);

        var forecast = new Forecast() {
            Provider = "Apixu",
            City = results.location.name,
            MinTemp = results.current.temp_c,
            MaxTemp = results.current.temp_c,
            Conditions = results.current.condition.text
        };

        return forecast;
    }

    public List<DailyForecast> GetDailyForecast()
    {
        var result = new List<DailyForecast>();

        return result;
    }
}