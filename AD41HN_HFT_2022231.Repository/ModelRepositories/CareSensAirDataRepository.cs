
using AD41HN_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Repository.ModelRepositories
{
    public class CareSensAirDataRepository : Repository<CareSensAirData>, IRepository<CareSensAirData>
    {
        public CareSensAirDataRepository(FWCDbContext ctx) : base(ctx)
        {
        }

        public override CareSensAirData Read(int id)
        {
            return ctx.CareSensAirDatas.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(CareSensAirData item)
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

        public IQueryable<CareSensAirData> GetByDateRange(DateTime from, DateTime to)
        {
            return ctx.CareSensAirDatas
                          .Where(d => d.Date_Time >= from && d.Date_Time <= to);
        }
    }
}
