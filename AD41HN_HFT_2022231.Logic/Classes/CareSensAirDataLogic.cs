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
    public class CareSensAirDataLogic : ICareSensAirDataLogic
    {
        IRepository<CareSensAirData> repo;

        public CareSensAirDataLogic(IRepository<CareSensAirData> repo)
        {
            this.repo = repo;
        }

        public void Create(CareSensAirData item)
        {
           
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public CareSensAirData Read(int id)
        {
            var player = this.repo.Read(id);
            if (player == null)
            {
                throw new ArgumentException("CareSensAirData not exists");
            }
            return player;

        }

        public IEnumerable<CareSensAirData> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public IEnumerable<CareSensAirData> GetByDateRange(DateTime from, DateTime to)
        {
            return ((CareSensAirDataRepository)this.repo).GetByDateRange(from, to).ToList();
        }


        public void Update(CareSensAirData item)
        {
            this.repo.Update(item);
        }

        Player ICareSensAirDataLogic.Read(int id)
        {
            throw new NotImplementedException();
        }
    }
}

