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
    public class OhioInsulinLogic : IOhioInsulinLogic
    {
        IRepository<OhioInsulin> repo;

        public OhioInsulinLogic(IRepository<OhioInsulin> repo)
        {
            this.repo = repo;
        }

        public void Create(OhioInsulin item)
        {
           
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public OhioInsulin Read(int id)
        {
            var player = this.repo.Read(id);
            if (player == null)
            {
                throw new ArgumentException("OhioInsulin not exists");
            }
            return player;

        }

        public IEnumerable<OhioInsulin> ReadById(int id)
        {
            return ((OhioInsulinRepository)this.repo).ReadById(id).ToList();

        }

        public IEnumerable<OhioInsulin> ReadAll()
        {
            return this.repo.ReadAll();
        }

       

        public void Update(OhioInsulin item)
        {
            this.repo.Update(item);
        }

        
    }
}

