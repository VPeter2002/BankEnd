using AD41HN_HFT_2022231.Logic.Interfaces;
using AD41HN_HFT_2022231.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AD41HN_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
       
        ITrainerLogic logic;

        public TrainerController(ITrainerLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<TrainerController>

        [HttpGet]
        public IEnumerable<Trainer> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Trainer Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Trainer value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Put([FromBody] Trainer value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }

        //[HttpGet("{id}")]
        //public IEnumerable<Team> GetTeamOfTrainer_id(int id)
        //{

        //    return this.logic.GetTeamOfTrainer_id(id);
        //}
    }
}
