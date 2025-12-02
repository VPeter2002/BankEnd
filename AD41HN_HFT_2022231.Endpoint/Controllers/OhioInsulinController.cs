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
    public class OhioInsulinController : ControllerBase
    {
        IOhioInsulinLogic logic;
        IHubContext<SignalRHub> hub;

        public OhioInsulinController(IOhioInsulinLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

       

        [HttpGet]
        public IEnumerable<OhioInsulin> ReadAll()
        {
            return this.logic.ReadAll().ToList();
        }

        [HttpGet("id")]
        public IEnumerable<OhioInsulin> ReadById(int id)
        {
            return this.logic.ReadById(id).ToList();
        }


        

        [HttpPut]
        public void Put([FromBody] OhioInsulin value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("OhioMeal", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var playerToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("OhioInsulin Deleted", playerToDelete);
        }
        // POST: api/OhioInsulin
        [HttpPost]
        public void Create([FromBody] OhioInsulin value)
        {
            if (string.IsNullOrEmpty(value.Key)) value.Key = Guid.NewGuid().ToString();
            if (value.M == 0) value.M = DateTimeOffset.Now.ToUnixTimeSeconds();

            this.logic.Create(value);
        }
    }
}
