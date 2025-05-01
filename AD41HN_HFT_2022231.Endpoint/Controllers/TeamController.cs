//using AD41HN_HFT_2022231.Endpoint.Services;
//using AD41HN_HFT_2022231.Logic.Interfaces;
//using AD41HN_HFT_2022231.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using System.Collections.Generic;
//using System.Linq;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace AD41HN_HFT_2022231.Endpoint.Controllers
//{
//    [Route("[controller]")]
//    [ApiController]
//    public class TeamController : ControllerBase
//    {
//        ITeamLogic logic;
//        IHubContext<SignalRHub> hub;

//        public TeamController(ITeamLogic logic, IHubContext<SignalRHub> hub)
//        {
//            this.logic = logic;
//            this.hub = hub; 
//        }

//        // GET: api/<TeamController>
        

//        [HttpGet]
//        public IEnumerable<Team> ReadAll()
//        {
//            return this.logic.ReadAll();
//        }

//        [HttpGet("{id}")]
//        public Team Read(int id)
//        {
//            return this.logic.Read(id);
//        }

//        [HttpPost]
//        public void Create([FromBody] Team value)
//        {
//            this.logic.Create(value);
//            this.hub.Clients.All.SendAsync("TeamCreated", value);
//        }

//        [HttpPut]
//        public void Put([FromBody] Team value)
//        {
//            this.logic.Update(value);
//            this.hub.Clients.All.SendAsync("TeamUpdated", value);
//        }

//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {


//            var teamToDelete = this.logic.Read(id);
//            this.logic.Delete(id);
//            this.hub.Clients.All.SendAsync("TeamDeleted", teamToDelete);
//        }
//        //[HttpGet("{teamname}")]

//        //public IQueryable GetGoalKeepersInTeam(string teamname)
//        //{
//        //    return this.logic.GetGoalKeepersInTeam(teamname);
//        //}
//    }
//}
