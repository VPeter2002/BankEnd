
using AD41HN_HFT_2022231.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Repository.ModelRepositories
{
    public class PatientRepository : Repository<Patient>, IRepository<Patient>
    {
        public PatientRepository(FWCDbContext ctx) : base(ctx)
        {
        }

        public override Patient Read(int id)
        {
            return ctx.Patients.FirstOrDefault(t => t.Id == id);
        }
        public IQueryable<Patient> ReadAll()
        {
            return ctx.Patients
                .Include(p => p.GlucoseLevel)
                .Include(p => p.FingerStick)
                .Include(p => p.Basal)
                .Include(p => p.TempBasal)
                .Include(p => p.Bolus)
                .Include(p => p.Meal)
                .Include(p => p.Sleep)
                .Include(p => p.Work)
                .Include(p => p.HypoEvent)
                .Include(p => p.Illness)
                .Include(p => p.Exercise)
                .Include(p => p.BasisHeartRate)
                .Include(p => p.BasisGsr)
                .Include(p => p.BasisSkinTemp)
                .Include(p => p.BasisAirTemp)
                .Include(p => p.BasisSteps)
                .Include(p => p.BasisSleep);
        }
        public override void Update(Patient item)
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

