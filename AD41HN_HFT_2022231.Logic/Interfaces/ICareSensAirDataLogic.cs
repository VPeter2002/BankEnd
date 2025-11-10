using AD41HN_HFT_2022231.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AD41HN_HFT_2022231.Logic.Interfaces
{
    public interface ICareSensAirDataLogic
    {
        void Create(CareSensAirData item);
        void Delete(int id);
        CareSensAirData Read(int id);
        IEnumerable<CareSensAirData> ReadAll();
        IEnumerable<CareSensAirData> GetByDateRange(DateTime from, DateTime to);
        void Update(CareSensAirData item);
    }
}