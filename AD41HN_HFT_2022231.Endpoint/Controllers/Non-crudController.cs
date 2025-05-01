//using AD41HN_HFT_2022231.Logic.Interfaces;
//using AD41HN_HFT_2022231.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections;
//using System.Collections.Generic;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace AD41HN_HFT_2022231.Endpoint.Controllers
//{
//    [Route("[controller]/[action]")]
//    [ApiController]
//    public class Non_crudController : ControllerBase
//    {
//        IPlayerLogic playerlogic;
//        ITeamLogic teamlogic;
//        ITrainerLogic trainerlogic;


//        public Non_crudController(IPlayerLogic playerLogic, ITrainerLogic trainerlogic, ITeamLogic teamlogic)
//        {
//            this.playerlogic = playerLogic;
//            this.teamlogic = teamlogic;
//            this.trainerlogic = trainerlogic;
//        }
//        //public Non_crudController(ITeamLogic teamlogic)
//        //{
//        //    this.teamlogic = teamlogic;
//        //}
//        //public Non_crudController(ITrainerLogic trainerlogic)
//        //{
//        //    this.trainerlogic = trainerlogic;
//        //}

//        [HttpGet("{post}")]
       
//        public IEnumerable GetPlayersOnThisPost(string post)
//        {
//            return this.playerlogic.GetPlayersOnThisPost(post);

//        }
//        [HttpGet("{Playername}")]

//        public IEnumerable GetTeamName(string Playername)
//        {
//            return this.playerlogic.GetTeamName(Playername);
//        }
//        [HttpGet("{Playername}")]

//        public IEnumerable GetTeamId(string Playername)
//        {
//            return this.playerlogic.GetTeamId(Playername);
//        }
//        [HttpGet("{teamname}")]

//        public IEnumerable GetGoalKeepersInTeam(string teamname)
//        {
//            return this.teamlogic.GetGoalKeepersInTeam(teamname);
//        }

//        [HttpGet("{id}")]
//        public IEnumerable<Team> GetTeamOfTrainer_id(int id)
//        {
//            return this.trainerlogic.GetTeamOfTrainer_id(id);
//        }

//        [HttpPost]
//        public IEnumerable GetHun()
//        {
//            return this.playerlogic.GetHun();
//        }


//        [HttpPost]
//        public IEnumerable GetGKs()
//        {
//            return this.playerlogic.GetGKs();
//        }

//        [HttpPost]
//        public IEnumerable GetEng()
//        {
//            return this.playerlogic.GetEng();
//        }

//        [HttpPost]
//        public IEnumerable GetTeamIds()
//        {
//            return this.teamlogic.GetTeamIds();
//        }

//        [HttpPost]
//        public IEnumerable GetGermanyTrainers()
//        {
//            return this.trainerlogic.GetGermanyTrainers();
//        }
  


//    }
//}
