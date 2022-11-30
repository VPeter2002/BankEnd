using AD41HN_HFT_2022231.Logic.Interfaces;
using AD41HN_HFT_2022231.Models;
using AD41HN_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Logic.Classes
{
    public class TrainerLogic : ITrainerLogic
    {
        IRepository<Trainer> repo;

        public TrainerLogic(IRepository<Trainer> repo)
        {
            this.repo = repo;
        }

        public void Create(Trainer item)
        {
            if (item.Name.Length < 2)
            {
                throw new ArgumentException("Trainer creating is not valid, name is too short!");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Trainer Read(int id)
        {
            var trainer = this.repo.Read(id);
            if (trainer.Name == null)
            {
                throw new ArgumentException("Trainer not exists");
            }
            return trainer;

        }

        public IEnumerable<Trainer> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Trainer item)
        {
            this.repo.Update(item);
        }

        //non cruds

        public IEnumerable<Team> GetTeamOfTrainer_id(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Id is not valid");
            }
            return this.repo
               .ReadAll()
               .Where(t => t.Id == id).Select(t => t.Team/*.Players*/);
        }

    }
}

