
using AD41HN_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Repository.ModelRepositories
{
    public class OhioMealRepository : Repository<OhioMeal>, IRepository<OhioMeal>
    {
        public OhioMealRepository(FWCDbContext ctx) : base(ctx)
        {
        }

        public override OhioMeal Read(int id)
        {
            return ctx.OhioMeal.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(OhioMeal item)
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

