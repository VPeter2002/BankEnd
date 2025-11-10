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
    public class OhioExerciseLogic : IOhioExerciseLogic
    {
        IRepository<OhioExercise> repo;

        public OhioExerciseLogic(IRepository<OhioExercise> repo)
        {
            this.repo = repo;
        }

        public void Create(OhioExercise item)
        {
           
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public OhioExercise Read(int id)
        {
            var player = this.repo.Read(id);
            if (player == null)
            {
                throw new ArgumentException("OhioExercise not exists");
            }
            return player;

        }

        public IEnumerable<OhioExercise> ReadAll()
        {
            return this.repo.ReadAll();
        }

       

        public void Update(OhioExercise item)
        {
            this.repo.Update(item);
        }

        
    }
}

