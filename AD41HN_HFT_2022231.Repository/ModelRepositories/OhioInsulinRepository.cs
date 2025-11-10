
using AD41HN_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Repository.ModelRepositories
{
    public class OhioInsulinRepository : Repository<OhioInsulin>, IRepository<OhioInsulin>
    {
        public OhioInsulinRepository(FWCDbContext ctx) : base(ctx)
        {
        }

        public override OhioInsulin Read(int id)
        {
            return ctx.OhioInsulin.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(OhioInsulin item)
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

        public IList<OhioInsulin> ReadById(int id)
        {
            return ctx.OhioInsulin
    .Where(g => g.PatID == id)
    .AsEnumerable()
    .OrderBy(g => g.DateTime)
    .ToList();
        }

    }
}


