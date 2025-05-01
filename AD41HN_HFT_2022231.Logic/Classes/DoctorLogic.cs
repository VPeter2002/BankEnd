using AD41HN_HFT_2022231.Logic.Interfaces;
using AD41HN_HFT_2022231.Models;
using AD41HN_HFT_2022231.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Logic.Classes
{
    public class DoctorLogic : IDoctorLogic
    {
        IRepository<Doctor> repo;

        public DoctorLogic(IRepository<Doctor> repo)
        {
            this.repo = repo;
        }

        public void Create(Doctor item)
        {
           
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Doctor Read(int id)
        {
            var player = this.repo.Read(id);
            if (player == null)
            {
                throw new ArgumentException("Player not exists");
            }
            return player;

        }

        public IEnumerable<Doctor> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Doctor item)
        {
            this.repo.Update(item);
        }

        Player IDoctorLogic.Read(int id)
        {
            throw new NotImplementedException();
        }
    }
}

