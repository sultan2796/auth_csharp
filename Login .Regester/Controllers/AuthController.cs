using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Login_.Regester.Data;
using Login_.Regester.Models;

namespace Login_.Regester.Controllers
{
	/// <summary>
	/// İstifadəçi autentifikasiya və qeydiyyat əməliyyatları üçün controller.
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly PasswordHasher<AppUser> _passwordHasher = new PasswordHasher<AppUser>();
		private readonly IConfiguration _configuration;
		
		/// <summary>
		/// AuthController-in yeni instance-ını yaradır.
		/// </summary>
		/// <param name="context">Verilənlər bazası konteksti.</param>
		/// <param name="configuration">Konfiqurasiya.</param>
		public AuthController(AppDbContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

		/// <summary>
		/// Yeni istifadəçi qeydiyyatı.
		/// </summary>
		/// <param name="user">Qeydiyyat üçün istifadəçi məlumatları.</param>
		/// <returns>Qeydiyyat nəticəsi.</returns>
		[HttpPost("register")]
		public IActionResult Register([FromBody] AppUser user)
		{
			if (user == null || string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
			{
				return BadRequest("İstifadəçi adı və şifrə boş ola bilməz.");
			}
			if (_context.Users.Any(u => u.Username == user.Username))
			{
				return BadRequest("Istifadeci ad movcudur.");
			}
			user.Password = _passwordHasher.HashPassword(user, user.Password);
			_context.Users.Add(user);
			_context.SaveChanges();
			return Ok("Qeydiyyat ugurlu.");
		}

		/// <summary>
		/// İstifadəçi girişi və JWT token əldə edilməsi.
		/// </summary>
		/// <param name="user">Giriş üçün istifadəçi məlumatları.</param>
		/// <returns>JWT token və ya xəta mesajı.</returns>
		[HttpPost("login")]
		public IActionResult Login([FromBody] AppUser user)
		{
			if (user == null || string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
			{
				return BadRequest("İstifadəçi adı və şifrə boş ola bilməz.");
			}
			var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);
			if (existingUser == null)
			{
				return Unauthorized("Istifadeci Ad Xetali.");
			}
			var result = _passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password, user.Password);
			if (result == PasswordVerificationResult.Failed)
			{
				return Unauthorized("Şifrə yanlışdır.");
			}
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "supersecretkey1234567890");
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, existingUser.Username),
					new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString())
				}),
				Expires = DateTime.UtcNow.AddHours(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var jwt = tokenHandler.WriteToken(token);
			return Ok(new { token = jwt });
		}

		/// <summary>
		/// Hazırda login olmuş istifadəçinin profil məlumatları.
		/// </summary>
		/// <returns>İstifadəçi ID-si və adı.</returns>
		[Authorize]
		[HttpGet("profile")]
		public IActionResult Profile()
		{
			var username = User.Identity?.Name;
			if (string.IsNullOrEmpty(username))
			{
				return Unauthorized();
			}
			var user = _context.Users.FirstOrDefault(u => u.Username == username);
			if (user == null)
			{
				return NotFound();
			}
			return Ok(new { user.Id, user.Username });
		}
	}
}
