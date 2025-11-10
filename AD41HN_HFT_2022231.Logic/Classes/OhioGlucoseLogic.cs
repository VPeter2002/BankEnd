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
    public class OhioGlucoseLogic : IOhioGlucoseLogic
    {
        IRepository<OhioGlucose> repo;

        public OhioGlucoseLogic(IRepository<OhioGlucose> repo)
        {
            this.repo = repo;
        }

        public void Create(OhioGlucose item)
        {
           
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public OhioGlucose Read(int id)
        {
            var player = this.repo.Read(id);
            if (player == null)
            {
                throw new ArgumentException("OhioGlucose not exists");
            }
            return player;

        }

        public IEnumerable<OhioGlucose> ReadById(int id)
        {
            return ((OhioGlucoseRepository)this.repo).ReadById(id).ToList();

        }

        public IEnumerable<OhioGlucose> ReadAll()
        {
            return this.repo.ReadAll();
        }

        

        //public IEnumerable<OhioGlucose> GetById()
        //{
        //    return this.repo.ReadAll();
        //}

        public void Update(OhioGlucose item)
        {
            this.repo.Update(item);
        }

        
    }
}

