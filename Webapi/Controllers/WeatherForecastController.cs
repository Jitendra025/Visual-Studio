using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            List<WeatherForecast> list = new List<WeatherForecast>();
            //using (SqlConnection connection = new SqlConnection("Data Source=192.168.159.1,1433;Initial Catalog=dokcer_test;Integrated Security=true;")) Windowauthentication can' be use in docker on personal laptop
                                                                                                                                                          //  as this gives error related to kerberose authentication that can be done uing Active directory and all
            using (SqlConnection connection = new SqlConnection("Data Source=192.168.159.1,1433;Initial Catalog=dokcer_test;User ID=sa;Password=1234;"))
            {
                SqlCommand command = new SqlCommand("select * from weatherforcast", connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    WeatherForecast weatherForecast=  new WeatherForecast
                    {
                        Date = Convert.ToDateTime(reader[0]),
                        TemperatureC = Convert.ToInt16(reader[1]),
                        Summary = Convert.ToString(reader[2])
                    };
                    list.Add(weatherForecast);

                }
            }
            var rng = new Random();
            return list.ToArray();
        }
    }
}
