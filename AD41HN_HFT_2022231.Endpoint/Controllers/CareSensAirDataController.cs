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
    public class CareSensAirDataController : ControllerBase
    {
        ICareSensAirDataLogic logic;
        IHubContext<SignalRHub> hub;

        public CareSensAirDataController(ICareSensAirDataLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet("id")]

        public CareSensAirData Read(int id)
        {
            return this.logic.Read(id);
        }


        [HttpGet]
        public IEnumerable<CareSensAirData> ReadAll()
        {
            return this.logic.ReadAll().ToList();
        }

        [HttpGet("bydate")]
        public IActionResult GetByDateRange([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var result = logic.GetByDateRange(from, to);
            return Ok(result);
        }


        [HttpPost]
        public void Create([FromBody] CareSensAirData value)
        {
           
        }

        [HttpPut]
        public void Put([FromBody] CareSensAirData value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("CareSensAirDataUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var playerToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("CareSensAirDataDeleted", playerToDelete);
        }
        // POST: api/CareSensAirData/upload
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Nincs kiválasztott fájl.");

            int count = 0;

            try
            {
                using (var stream = new StreamReader(file.OpenReadStream()))
                {
                    // Fejléc átugrása (ha van) - Opcionális, most feltételezzük, hogy az első sor fejléc
                    // await stream.ReadLineAsync(); 

                    while (!stream.EndOfStream)
                    {
                        var line = await stream.ReadLineAsync();
                        var values = line.Split(','); // Vagy ';' attól függően, mi az elválasztó

                        // FONTOS: Itt kell hozzáigazítani a te CSV formátumodhoz!
                        // Feltételezés: 1. oszlop = Dátum, 2. oszlop = Érték (mmol/l)

                        if (values.Length >= 2)
                        {
                            try
                            {
                                // Dátum és Érték kinyerése
                                // CareSens formátum függő! Lehet, hogy cserélni kell a sorrendet.
                                if (DateTime.TryParse(values[0], out DateTime date) &&
                                    double.TryParse(values[1].Replace(".", ","), out double glucoseVal))
                                {
                                    var newData = new CareSensAirData
                                    {
                                        Date_Time = date,
                                        Value = glucoseVal,
                                        Unit = "mmol/L",
                                        Device = "Imported",
                                        SerialNumber = "CSV"
                                    };

                                    this.logic.Create(newData);
                                    count++;
                                }
                            }
                            catch
                            {
                                // Ha egy sor hibás, átugorjuk
                                Console.WriteLine($"Hibás sor: {line}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba a feldolgozáskor: {ex.Message}");
            }

            return Ok(new { message = $"Sikeres feltöltés! {count} új mérés rögzítve." });
        }
    }
}
