namespace Login_.Regester
{
	/// <summary>
	/// Hava proqnozu məlumatlarını təmsil edir.
	/// </summary>
	public class WeatherForecast
	{
		/// <summary>
		/// Proqnoz tarixi.
		/// </summary>
		public DateOnly Date { get; set; }

		/// <summary>
		/// Temperatur Selsi dərəcəsində.
		/// </summary>
		public int TemperatureC { get; set; }

		/// <summary>
		/// Temperatur Farenheit dərəcəsində.
		/// </summary>
		public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

		/// <summary>
		/// Hava vəziyyətinin təsviri.
		/// </summary>
		public string? Summary { get; set; }
	}
}
