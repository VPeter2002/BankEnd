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
    public class PatientLogic : IPatientLogic
    {
        IRepository<Patient> repo;

        public PatientLogic(IRepository<Patient> repo)
        {
            this.repo = repo;
        }

        public void Create(Patient item)
        {
           
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Patient Read(int id)
        {
            var player = this.repo.Read(id);
            if (player == null)
            {
                throw new ArgumentException("Patient not exists");
            }
            return player;

        }

        public IEnumerable<Patient> ReadAll()
        {
            return this.repo.ReadAll();
        }

       

        public void Update(Patient item)
        {
            this.repo.Update(item);
        }

        
    }
}

