using System.Collections.Generic;

public class Forecast
{
    public string Provider { get; set; }
    public string City { get; set; }
    public decimal MinTemp { get; set; }
    public decimal MaxTemp { get; set; }
    public string Conditions { get; set; }

    public List<DailyForecast> DailyForecast { get; set; }

    public Forecast()
    {
        DailyForecast = new List<DailyForecast>();
    }
}