using AD41HN_HFT_2022231.Endpoint.Services;
using AD41HN_HFT_2022231.Logic.Interfaces;
using AD41HN_HFT_2022231.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AD41HN_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OhioGlucoseController : ControllerBase
    {
        IOhioGlucoseLogic logic;
        IHubContext<SignalRHub> hub;

        public OhioGlucoseController(IOhioGlucoseLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

       

        [HttpGet]
        public IEnumerable<OhioGlucose> ReadAll()
        {
            return this.logic.ReadAll().ToList();
        }

        [HttpGet("id")]
        public IEnumerable<OhioGlucose> ReadById(int id)
        {
            return this.logic.ReadById(id).ToList();
        }





        [HttpPost]
        public void Create([FromBody] OhioGlucose value)
        {
            Console.WriteLine("--- ÚJ ADAT MENTÉSE ---");

            // 1. JAVÍTÁS: Ha nincs kulcs, generálunk egyet (GUID)
            if (string.IsNullOrEmpty(value.Key))
            {
                value.Key = Guid.NewGuid().ToString(); // Pl: "e8a94b7f-..."
                Console.WriteLine($"Kulcs generálva: {value.Key}");
            }

            // 2. JAVÍTÁS (Opcionális): Ha az 'm' (módosítás dátuma) üres, kitöltjük
            if (value.M == 0)
            {
                value.M = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
            }

            // Ha esetleg a JSON konverzió miatt a PatID még mindig 0 lenne
            // (bár a Startup.cs javítás elvileg megoldotta), itt még menthetjük a helyzetet:
            if (value.PatID == 0)
            {
                // Ideiglenes vészmegoldás teszteléshez, ha nagyon kell:
                // value.PatID = 544; 
                Console.WriteLine("FIGYELEM: A PatID 0! Ellenőrizd a frontend küldést.");
            }

            this.logic.Create(value);
            Console.WriteLine("Sikeresen átadva a logikának.");
        }

        [HttpPut]
        public void Put([FromBody] OhioGlucose value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("OhioGlucoseUpdated", value);
        }

        // POST: api/OhioGlucose/upload
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file, [FromForm] int patId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Nincs kiválasztott fájl.");

            int count = 0;
            var debugLog = new StringBuilder();

            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var jsonContent = await reader.ReadToEndAsync();

                    var options = new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
                    };

                    var items = System.Text.Json.JsonSerializer.Deserialize<List<System.Text.Json.JsonElement>>(jsonContent, options);

                    foreach (var item in items)
                    {
                        System.Text.Json.JsonElement typeProp = default;
                        bool typeFound = false;

                        foreach (var prop in item.EnumerateObject())
                        {
                            if (prop.Name.Equals("type", StringComparison.OrdinalIgnoreCase))
                            {
                                typeProp = prop.Value;
                                typeFound = true;
                                break;
                            }
                        }

                        if (typeFound)
                        {
                            var typeValue = typeProp.GetString();

                            if (string.Equals(typeValue, "glucose", StringComparison.OrdinalIgnoreCase))
                            {
                                try
                                {
                                    var glucoseData = System.Text.Json.JsonSerializer.Deserialize<OhioGlucose>(item.GetRawText(), options);

                                    // --- ADATOK FELÜLÍRÁSA A BIZTONSÁGOS MENTÉSHEZ ---
                                    glucoseData.PatID = patId;

                                    // MINDIG generálunk új kulcsot, hogy ne legyen ütközés!
                                    glucoseData.Key = Guid.NewGuid().ToString();

                                    // Időbélyeg kezelése
                                    if (glucoseData.M == 0) glucoseData.M = (int)DateTimeOffset.Now.ToUnixTimeSeconds();

                                    this.logic.Create(glucoseData);
                                    count++;
                                }
                                catch (Exception ex)
                                {
                                    var innerMessage = ex.InnerException != null ? ex.InnerException.Message : "Nincs belső hiba.";
                                    debugLog.AppendLine($"Hiba a mentésnél: {ex.Message} -> Belső: {innerMessage}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Kritikus hiba a fájl olvasásakor: {ex.Message}");
            }

            if (count == 0)
            {
                return Ok(new { message = $"Feltöltés kész, DE 0 adat lett rögzítve. \nOkok:\n{debugLog}" });
            }

            return Ok(new { message = $"Sikeres feltöltés! {count} új mérés rögzítve." });
        }

    }
}
