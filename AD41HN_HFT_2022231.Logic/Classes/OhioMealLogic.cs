using AD41HN_HFT_2022231.Logic.Interfaces;
using AD41HN_HFT_2022231.Models;
using AD41HN_HFT_2022231.Repository;
using AD41HN_HFT_2022231.Repository.ModelRepositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Logic.Classes
{
    public class OhioMealLogic : IOhioMealLogic
    {
        IRepository<OhioMeal> repo;

        public OhioMealLogic(IRepository<OhioMeal> repo)
        {
            this.repo = repo;
        }

        public void Create(OhioMeal item)
        {
           
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public OhioMeal Read(int id)
        {
            var player = this.repo.Read(id);
            if (player == null)
            {
                throw new ArgumentException("OhioMeal not exists");
            }
            return player;

        }

        public IEnumerable<OhioMeal> ReadById(int id)
        {
            return this.repo.ReadAll().Where(m => m.PatID == id).ToList();
        }

        public IEnumerable<OhioMeal> ReadAll()
        {
            return this.repo.ReadAll();
        }

       

        public void Update(OhioMeal item)
        {
            this.repo.Update(item);
        }

        
    }
}

