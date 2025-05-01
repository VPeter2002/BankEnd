//using AD41HN_HFT_2022231.Endpoint.Services;
//using AD41HN_HFT_2022231.Logic.Interfaces;
//using AD41HN_HFT_2022231.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace AD41HN_HFT_2022231.Endpoint.Controllers
//{
//    [Route("[controller]")]
//    [ApiController]
//    public class PlayerController : ControllerBase
//    {
//        IPlayerLogic logic;
//        IHubContext<SignalRHub> hub;

//        public PlayerController(IPlayerLogic logic, IHubContext<SignalRHub> hub)
//        {
//            this.logic = logic;
//            this.hub = hub;
//        }

//        // GET: api/<PlayerController>
        
//        [HttpGet]
//        public IEnumerable<Player> ReadAll()
//        {
//            return this.logic.ReadAll();
//        }

//        [HttpGet("{id}")]
//        public Player Read(int id)
//        {
//            return this.logic.Read(id);
//        }

//        [HttpPost]
//        public void Create([FromBody] Player value)
//        {
//            this.logic.Create(value);
//            this.hub.Clients.All.SendAsync("PlayerCreated", value);
//        }

//        [HttpPut]
//        public void Put([FromBody] Player value)
//        {
//            this.logic.Update(value);
//            this.hub.Clients.All.SendAsync("PlayerUpdated", value);
//        }

//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//            var playerToDelete = this.logic.Read(id);
//            this.logic.Delete(id);
//            this.hub.Clients.All.SendAsync("PlayerDeleted", playerToDelete);
//        }
//        //[HttpGet("{post}")]

//        //public IEnumerable GetPlayersOnThisPost(string post)
//        //{
//        //    return this.logic.GetPlayersOnThisPost(post);
               
//        //}
//        //[HttpGet("{Playername}")]

//        //public IEnumerable GetTeamName(string Playername)
//        //{
//        //    return this.logic.GetTeamName(Playername);
//        //}
//        //[HttpGet("{Playername}")]

//        //public IEnumerable GetTrainerName(string Playername)
//        //{
//        //    return this.logic.GetTrainerName(Playername);
//        //}
//    }
//}
