using Azure.TableStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAzure.Shared;

namespace TestAzure.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        readonly ITableStorageService tableService;
        readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            ITableStorageService tableService,
            ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            this.tableService = tableService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var rng = new Random();
            var forecasts = Enumerable.Range(1, 5)
                .Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    RowKey = DateTime.Now.AddDays(index).ToString("dd:MM:yyyy"),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();

            foreach (var item in forecasts)
                await tableService.Insert(item);

            return forecasts;
        }
    }
}
