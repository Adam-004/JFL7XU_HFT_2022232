using JFL7XU_HFT_2022232.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Endpoint.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAuthenticationController(UserManager<User> userManager, JwtSettings jwtSettings) : ControllerBase
    {
        private readonly UserManager<User> UserManager = userManager;
        private readonly JwtSettings JwtSettings = jwtSettings;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                UserName = dto.Username,
                Email = dto.Email
            };

            var result = await UserManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await UserManager.FindByNameAsync(dto.Username);
            if (user == null)
                return Unauthorized(new { Message = "Invalid username or password" });

            var passwordValid = await UserManager.CheckPasswordAsync(user, dto.Password);
            if (!passwordValid)
                return Unauthorized(new { Message = "Invalid username or password" });

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(JwtSettings.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = System.DateTime.UtcNow.AddHours(2),
                Issuer = JwtSettings.Issuer,
                Audience = JwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Ok(new { Token = jwt, Username = user.UserName });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userId = User.FindFirstValue("id");
            if (userId == null) return Unauthorized();

            var user = await UserManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email
            });
        }
    }

    public class RegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
