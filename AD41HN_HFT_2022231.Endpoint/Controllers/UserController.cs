using AD41HN_HFT_2022231.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AD41HN_HFT_2022231.Endpoint.Controllers
{

    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<Doctor> users = new List<Doctor>(); // Ideiglenes tárolás
        
        [HttpPost("register")]
        public IActionResult Register([FromBody] Doctor user)
        {
            if (users.Exists(u => u.Name == user.Name))
            {
                return BadRequest(new { message = "Ez az email már foglalt!" });
            }

            users.Add(user);
            return Ok(new { message = "Sikeres regisztráció!" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Doctor loginRequest)
        {
            var user = users.Find(u => u.Name == loginRequest.Name && u.Password == loginRequest.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Hibás email vagy jelszó!" });
            }

            return Ok(new { message = "Sikeres bejelentkezés!" });
        }

        [HttpGet("adatok")]
        public IActionResult GetAdatok()
        {
            return Ok(new { message = "Ez egy GET kérés" });
        }
    }

}
