using AD41HN_HFT_2022231.Db;
using System;
using System.Linq;

namespace ToConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FWCDbContext ctx = new FWCDbContext();
            var q0 = ctx.Players.Where(t=>t.Team.Name=="Magyar").Select(t=>t.Name);
            
            foreach (var item in q0)
            {

            }
        }
    }
}
