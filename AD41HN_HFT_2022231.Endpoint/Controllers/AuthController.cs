// AD41HN_HFT_2022231.Endpoint/Controllers/AuthController.cs
using AD41HN_HFT_2022231.Db.Data;
using AD41HN_HFT_2022231.Models; // Hozzáférés a DTO-khoz
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; // IConfiguration-höz
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Endpoint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        // Konstruktor az Identity és a Konfiguráció injektálásához
        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var userExists = await _userManager.FindByNameAsync(registerDto.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status400BadRequest, new AuthResponseDto { IsSuccess = false, Message = "Felhasználónév már foglalt!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDto.Username,
                UserRole = registerDto.Role // Mentjük a szerepkört
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponseDto { IsSuccess = false, Message = "Hiba a felhasználó létrehozásakor! A jelszónak tartalmaznia kell egyedi karaktert, kis- és nagybetűt, valamint számot." });

            // Szerepkörök létrehozása (Orvos/Páciens), ha még nem léteznek
            if (!await _roleManager.RoleExistsAsync("Orvos"))
                await _roleManager.CreateAsync(new IdentityRole("Orvos"));
            if (!await _roleManager.RoleExistsAsync("Páciens"))
                await _roleManager.CreateAsync(new IdentityRole("Páciens"));

            // Felhasználó hozzáadása a szerepkörhöz
            if (registerDto.Role == "Orvos" || registerDto.Role == "Páciens")
            {
                await _userManager.AddToRoleAsync(user, registerDto.Role);
            }

            return Ok(new AuthResponseDto { IsSuccess = true, Message = "Sikeres regisztráció!" });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                authClaims.Add(new Claim("UserRole", user.UserRole)); // Hozzáadjuk a mi szerepkörünket is

                var token = GenerateToken(authClaims);

                return Ok(new AuthResponseDto
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Role = user.UserRole,
                    IsSuccess = true,
                    Message = "Sikeres bejelentkezés"
                });
            }
            return Unauthorized(new AuthResponseDto { IsSuccess = false, Message = "Érvénytelen felhasználónév vagy jelszó." });
        }

        // Token generáló segédfüggvény
        private JwtSecurityToken GenerateToken(IEnumerable<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}