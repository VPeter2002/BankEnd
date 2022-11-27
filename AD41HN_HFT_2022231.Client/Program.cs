
using AD41HN_HFT_2022231.Db;
using AD41HN_HFT_2022231.Logic.Classes;
using AD41HN_HFT_2022231.Models;
using AD41HN_HFT_2022231.Repository;
using AD41HN_HFT_2022231.Repository.ModelRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AD41HN_HFT_2022231.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
           FWCDbContext ctx = new FWCDbContext();

            var q1 = ctx.Trainers;

            var playerrepo = new PlayerRepository(ctx);
            var teamrepo = new TeamRepository(ctx);


            var Playerlogic = new PlayerLogic(playerrepo);
            var Teamlogic = new TeamLogic(teamrepo);

            var q4 = Teamlogic.GetGoalKeepersInTeam("Német");
            
            var q2 = Playerlogic.GetTeamName("Németh Kristof");
            Playerlogic.Create(new Player() { Id = 300, Name = "Carlos", Post = "GK", TeamId = 1 });
            var q3 = Playerlogic.ReadAll();
            ;

            foreach (var item in q1)
            {
                Console.WriteLine(item.Name);
            }


        }
}
}
