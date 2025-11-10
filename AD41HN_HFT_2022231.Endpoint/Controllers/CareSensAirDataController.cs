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
