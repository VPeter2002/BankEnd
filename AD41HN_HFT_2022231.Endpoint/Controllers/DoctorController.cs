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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AD41HN_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        IDoctorLogic logic;
        IHubContext<SignalRHub> hub;

        public DoctorController(IDoctorLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        // GET: api/<PlayerController>

        [HttpGet]
        public IEnumerable<Doctor> ReadAll()
        {
            return this.logic.ReadAll();
        }


        [HttpPost]
        public void Create([FromBody] Doctor value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("DoctorCreated", value);

            string jsonFilePath = "C:\\Users\\Peti ROG\\Desktop\\Tanulós\\Diabetes Webapplication Backend\\AD41HN_HFT_2022231.Repository\\Doctors.json";

            // Betöltjük a meglévő orvosokat, ha a fájl létezik
            List<Doctor> doctors = new List<Doctor>();
            if (System.IO.File.Exists(jsonFilePath))
            {
                string jsonContent = System.IO.File.ReadAllText(jsonFilePath);
                doctors = JsonConvert.DeserializeObject<List<Doctor>>(jsonContent) ?? new List<Doctor>();
            }

           
            doctors.Add(value);

            // Frissített JSON fájl mentése
            string updatedJson = JsonConvert.SerializeObject(doctors, Formatting.Indented);
            System.IO.File.WriteAllText(jsonFilePath, updatedJson);

            Console.WriteLine("Új orvos hozzáadva!");
        }

        [HttpPut]
        public void Put([FromBody] Doctor value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("PlayerUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var playerToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("PlayerDeleted", playerToDelete);
        }
        //[HttpGet("{post}")]

        //public IEnumerable GetPlayersOnThisPost(string post)
        //{
        //    return this.logic.GetPlayersOnThisPost(post);

        //}
        //[HttpGet("{Playername}")]

        //public IEnumerable GetTeamName(string Playername)
        //{
        //    return this.logic.GetTeamName(Playername);
        //}
        //[HttpGet("{Playername}")]

        //public IEnumerable GetTrainerName(string Playername)
        //{
        //    return this.logic.GetTrainerName(Playername);
        //}
    }
}
