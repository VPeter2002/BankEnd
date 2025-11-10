using AD41HN_HFT_2022231.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AD41HN_HFT_2022231.Logic.Interfaces
{
    public interface IOhioMealLogic
    {
        void Create(OhioMeal item);
        void Delete(int id);
        OhioMeal Read(int id);
        IEnumerable<OhioMeal> ReadAll();
        IEnumerable<OhioMeal> ReadById(int id);


        void Update(OhioMeal item);
    }
}