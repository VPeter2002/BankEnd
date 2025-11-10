using AD41HN_HFT_2022231.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AD41HN_HFT_2022231.Logic.Interfaces
{
    public interface IPatientLogic
    {
        void Create(Patient item);
        void Delete(int id);
        Patient Read(int id);
        IEnumerable<Patient> ReadAll();
       
        void Update(Patient item);
    }
}