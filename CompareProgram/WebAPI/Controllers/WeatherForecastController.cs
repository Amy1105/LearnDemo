using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers
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

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost(Name = "GeFiles")]
        public ActionResult GeFile()
        {
            string name = "∂Õ¡∂º∆ªÆ.xlsx";
            string filePath = @"D:\file1.xlsx";
            try
            {
                Response.Headers.TryAdd("Access-Control-Expose-Headers", "Content-Disposition");
                Response.Headers.TryAdd("Content-Disposition", "attachment;filename=" + name);
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return new JsonResult(new { result = false, msg = "œ¬‘ÿ ß∞‹£°" });
            }
        }
       
    }
}