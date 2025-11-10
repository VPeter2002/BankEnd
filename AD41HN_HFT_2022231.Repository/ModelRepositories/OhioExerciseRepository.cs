
using AD41HN_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Repository.ModelRepositories
{
    public class OhioExerciseRepository : Repository<OhioExercise>, IRepository<OhioExercise>
    { 
        public OhioExerciseRepository(FWCDbContext ctx) : base(ctx)
        {
        }

        public override OhioExercise Read(int id)
        {
            return ctx.OhioExercise.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(OhioExercise item)
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

