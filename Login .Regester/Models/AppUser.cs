namespace Login_.Regester.Models
{
	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// İstifadəçi modelini təmsil edir.
	/// </summary>
	public class AppUser
	{
		/// <summary>
		/// İstifadəçi ID-si
		/// </summary>
		[Key]
		public int Id { get; set; }
		/// <summary>
		/// İstifadəçi adı
		/// </summary>
		[Required]
		public string Username { get; set; } = string.Empty;
		/// <summary>
		/// İstifadəçi şifrəsi (hash-lənmiş)
		/// </summary>
		[Required]
		public string Password { get; set; } = string.Empty;
	}
}
