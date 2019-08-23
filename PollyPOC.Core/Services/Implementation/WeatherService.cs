using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using PollyPOC.Core.Models;
using PollyPOC.Models;
using Console = Colorful.Console;

namespace PollyPOC.Core.Services.Implementation
{
    public class WeatherService : IWeatherService
    {
        private static readonly HttpClient client = new HttpClient();
        private int currentTry;

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
            Console.WriteLine("Getting forecast from WeatherService", Color.Green);

            if (shouldFail)
                if (++currentTry % 5 != 0)
                {
                    Console.WriteLine("Throwing exception at WeatherService.", Color.Red);
                    throw new Exception("WeatherService has returned an error.");
                }

            var data = client
                .GetStringAsync(
                    "https://api.openweathermap.org/data/2.5/weather?q=Guadalajara,MX&units=metric&appid=6867473c44c7ee8034fba321d9802479")
                .Result;
            dynamic results = JObject.Parse(data);

            var forecast = new Forecast
            {
                Provider = "OpenWeatherMap",
                City = results.name,
                MinTemp = results.main.temp_min,
                MaxTemp = results.main.temp_max,
                Conditions = results.weather[0].description
            };

            Console.WriteLine("Forecast obtained successfully from WeatherService.", Color.Green);

            return forecast;
        }


        public List<DailyForecast> GetDailyForecast()
        {
            var result = new List<DailyForecast>();

            return result;
        }
    }
}