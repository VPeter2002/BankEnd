using AD41HN_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Repository.ModelRepositories
{
    public class TeamRepository : Repository<Team>, IRepository<Team>
    {
        public TeamRepository(FWCDbContext ctx) : base(ctx)
        {
        }

        public override Team Read(int id)
        {
            return ctx.Teams.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(Team item)
        {
            var old = Read(item.Id);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
