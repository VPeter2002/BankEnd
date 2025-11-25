using AD41HN_HFT_2022231.Db.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Endpoint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Csak bejelentkezett felhasználóknak!
    public class PatientDoctorController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientDoctorController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // 1. PÁCIENS: Orvos hozzárendelése email alapján C:\Users\Peti ROG\Desktop\Tanulós\Diabetes Webapplication Backend másolata\AD41HN_HFT_2022231.Endpoint\DiaLogUsers.db
        // POST: api/PatientDoctor/set-doctor
        [HttpPost("set-doctor")]
        [Authorize(Roles = "Páciens")] // Csak páciens hívhatja
        public async Task<IActionResult> SetDoctor([FromBody] string doctorEmail)
        {
            // Megkeressük a jelenlegi bejelentkezett pácienst
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            // Megnézzük, létezik-e az orvos
            var doctor = await _userManager.FindByEmailAsync(doctorEmail);
            if (doctor == null || doctor.UserRole != "Orvos")
            {
                return BadRequest("Nem található ilyen email című orvos a rendszerben.");
            }

            // Beállítjuk a kapcsolatot
            user.DoctorEmail = doctor.Email;
            await _userManager.UpdateAsync(user);

            return Ok($"Sikeresen hozzárendelve Dr. {doctor.UserName}-hez.");
        }

        // 2. ORVOS: Saját páciensek listázása
        // GET: api/PatientDoctor/my-patients
        [HttpGet("my-patients")]
        [Authorize(Roles = "Orvos")] // Csak orvos hívhatja
        public async Task<IActionResult> GetMyPatients()
        {
            // Megkeressük a jelenlegi bejelentkezett orvost
            var doctor = await _userManager.GetUserAsync(User);
            if (doctor == null) return Unauthorized();

            // Kikeressük azokat a felhasználókat, akiknek ez az orvos van beállítva
            // Megjegyzés: Az Identity UserManager alapból nem támogatja a közvetlen LINQ-t minden mezőre,
            // ezért lehet, hogy a Users IQueryable-t kell használni.
            var patients = _userManager.Users
                .Where(u => u.DoctorEmail == doctor.Email && u.UserRole == "Páciens")
                .Select(u => new
                {
                    u.UserName,
                    u.Email,
                    u.Id // Fontos lehet a későbbi adatlekérdezéshez
                })
                .ToList();

            return Ok(patients);
        }
    }
}