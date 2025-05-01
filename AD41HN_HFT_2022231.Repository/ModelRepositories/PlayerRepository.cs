
using AD41HN_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Repository.ModelRepositories
{
    public class PlayerRepository : Repository<Player>, IRepository<Player>
    {
        public PlayerRepository(FWCDbContext ctx) : base(ctx)
        {
        }

        public override Player Read(int id)
        {
            return ctx.Players.FirstOrDefault(t => t.Id== id);
        }

        public override void Update(Player item)
        {

            var old = Read(item.Id);
            foreach (var prop in old.GetType().GetProperties())
            {
                try
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
                catch (Exception)
                {

                   
                }
            }
            ctx.SaveChanges();
        }
    }
}
