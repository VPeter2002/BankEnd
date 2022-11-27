using AD41HN_HFT_2022231.Db;
using AD41HN_HFT_2022231.Logic.Classes;
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

            var Playerlogic = new PlayerLogic(playerrepo);

            foreach (var item in q1)
            {
                Console.WriteLine(item.Name);
            }


        }
}
}
