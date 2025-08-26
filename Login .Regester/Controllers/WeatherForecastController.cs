using Microsoft.AspNetCore.Mvc;

namespace Login_.Regester.Controllers
{
	/// <summary>
	/// Hava proqnozu məlumatları üçün controller.
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;

		/// <summary>
		/// WeatherForecastController-in yeni instance-ını yaradır.
		/// </summary>
		/// <param name="logger">Logger instance-ı.</param>
		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Hava proqnozu məlumatlarını qaytarır.
		/// </summary>
		/// <returns>Hava proqnozu siyahısı.</returns>
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
	}
}
