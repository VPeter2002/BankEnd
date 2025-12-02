using AD41HN_HFT_2022231.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AD41HN_HFT_2022231.Repository.ModelRepositories
{
    public class OhioMealRepository : Repository<OhioMeal>, IRepository<OhioMeal>
    {
        public OhioMealRepository(FWCDbContext ctx) : base(ctx)
        {
        }

        // Egy elem lekérése ID alapján (+Foods betöltése)
        public override OhioMeal Read(int id)
        {
            return ctx.OhioMeal
                .Include(m => m.Foods)
                .FirstOrDefault(t => t.Id == id);
        }

        // Összes elem lekérése (+Foods betöltése)
        public override IQueryable<OhioMeal> ReadAll()
        {
            return ctx.OhioMeal
                .Include(m => m.Foods);
        }

        // 👇 EZ A METÓDUS HIÁNYZOTT (CS0534 hiba javítása) 👇
        public override void Update(OhioMeal item)
        {
            var old = Read(item.Id);
            if (old != null)
            {
                // Frissítjük az értékeket az új elem értékeivel
                ctx.Entry(old).CurrentValues.SetValues(item);
                ctx.SaveChanges();
            }
        }
    }
}