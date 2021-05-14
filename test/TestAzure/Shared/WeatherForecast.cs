using Azure.TableStorage.Abstract;
using System;

namespace TestAzure.Shared
{
    public class WeatherForecast : AzureTableEntity
    {
        public WeatherForecast() : base(typeof(WeatherForecast).Name.ToLowerInvariant())
        {
            
        }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    public class WeatherSummary : AzureTableEntity
    {
        public WeatherSummary() : base(typeof(WeatherSummary).Name.ToLowerInvariant())
        { }

        public string Name { get; set; }
    }
}
