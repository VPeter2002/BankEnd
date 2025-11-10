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
using AD41HN_HFT_2022231.Logic.Classes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AD41HN_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        IPatientLogic logic;
        IHubContext<SignalRHub> hub;

        public PatientController(IPatientLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

       

        [HttpGet]
        public IEnumerable<Patient> ReadAll()
        {
            return this.logic.ReadAll().ToList();
        }

       


        

        [HttpPut]
        public void Put([FromBody] Patient value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("Patient Updated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var playerToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("Patient Deleted", playerToDelete);
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
