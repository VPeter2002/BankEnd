using AD41HN_HFT_2022231.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AD41HN_HFT_2022231.Logic.Interfaces
{
    public interface IOhioExerciseLogic
    {
        void Create(OhioExercise item);
        void Delete(int id);
        OhioExercise Read(int id);
        IEnumerable<OhioExercise> ReadAll();
       
        void Update(OhioExercise item);
    }
}