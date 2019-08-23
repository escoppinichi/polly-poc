using System.Collections.Generic;
using PollyPOC.Core.Models;

namespace PollyPOC.Models
{
    public class Forecast
    {
        public Forecast()
        {
            DailyForecast = new List<DailyForecast>();
        }

        public string Provider { get; set; }
        public string City { get; set; }
        public decimal MinTemp { get; set; }
        public decimal MaxTemp { get; set; }
        public string Conditions { get; set; }

        public List<DailyForecast> DailyForecast { get; set; }
    }
}