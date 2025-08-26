namespace Login_.Regester.Data
{
	using Login_.Regester.Models;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	/// Entity Framework Core verilənlər bazası konteksti.
	/// </summary>
	public class AppDbContext : DbContext
	{
		/// <summary>
		/// AppDbContext-in yeni instance-ını yaradır.
		/// </summary>
		/// <param name="options">DbContext konfiqurasiya seçimləri.</param>
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		
		/// <summary>
		/// İstifadəçilər cədvəli.
		/// </summary>
		public DbSet<AppUser> Users { get; set; }
	}
}
