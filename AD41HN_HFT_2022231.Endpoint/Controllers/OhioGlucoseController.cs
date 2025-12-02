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

            if (patId == 0)
                return BadRequest("Nincs megadva Páciens ID.");

            int count = 0;

            try
            {
                using (var stream = new StreamReader(file.OpenReadStream()))
                {
                    // Ha van fejléc a CSV-ben, ugorjuk át (opcionális):
                    // await stream.ReadLineAsync(); 

                    while (!stream.EndOfStream)
                    {
                        var line = await stream.ReadLineAsync();
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        var values = line.Split(','); // Vagy ';'

                        // Feltételezett CSV formátum: Dátum, Érték
                        // Pl: 2025-05-20 08:00, 5.5
                        if (values.Length >= 2)
                        {
                            try
                            {
                                if (DateTime.TryParse(values[0], out DateTime date) &&
                                    double.TryParse(values[1].Replace(".", ","), out double glucoseVal))
                                {
                                    var newData = new OhioGlucose
                                    {
                                        Key = Guid.NewGuid().ToString(),
                                        PatID = patId,

                                        // HIBÁS SOR VOLT: DateTime = date.ToString(...)

                                        // HELYES SOR (A dátumot átváltjuk másodpercre):
                                        TimeStamp = (int)((DateTimeOffset)date).ToUnixTimeSeconds(),

                                        Value = glucoseVal,
                                        Type = "glucose",
                                        Collection = "CSV_IMPORT",
                                        M = (int)DateTimeOffset.Now.ToUnixTimeSeconds() // Itt is int-re kell castolni, ha a modelledben int
                                    };

                                    this.logic.Create(newData);
                                    count++;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Hiba a sor feldolgozásakor ({line}): {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba: {ex.Message}");
            }

            return Ok(new { message = $"Sikeres feltöltés! {count} adat rögzítve az {patId}-es pácienshez." });
        }


    }
}
