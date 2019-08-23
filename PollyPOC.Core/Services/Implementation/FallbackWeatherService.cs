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
    public class FallbackWeatherService : IWeatherService
    {
        private static readonly HttpClient client = new HttpClient();

        // This variable is used to simulate an error. Given N tries, the service will 'successfully' retrieve the data.
        // Otherwise return an exception.
        private int currentTry;

        public FallbackWeatherService()
        {
            client.DefaultRequestHeaders.Accept.Clear();
        }

        public Forecast GetForecast(bool shouldFail)
        {
            // This uses Apixu API
            // Get an API Key in their website to test this service.

            // This variable is used to simulate an error. Given N tries, the service will 'successfully' retrieve the data.
            // Otherwise return an exception.
            Console.WriteLine("Getting forecast from FallbackWeatherService", Color.Green);

            if (shouldFail)
                if (++currentTry % 3 != 0)
                {
                    Console.WriteLine("Throwing exception at FallbackWeatherService.", Color.Red);
                    throw new Exception("FallbackWeatherService has returned an error.");
                }


            var data = client.GetStringAsync("https://api.apixu.com/v1/current.json?key=80b3bd789c5b486d89e01617192308&q=Guadalajara").Result;
            dynamic results = JObject.Parse(data);

            var forecast = new Forecast
            {
                Provider = "Apixu",
                City = results.location.name,
                MinTemp = results.current.temp_c,
                MaxTemp = results.current.temp_c,
                Conditions = results.current.condition.text
            };

            Console.WriteLine("Forecast obtained successfully from FallbackWeatherService.", Color.Green);

            return forecast;
        }

        public List<DailyForecast> GetDailyForecast()
        {
            var result = new List<DailyForecast>();

            return result;
        }
    }
}