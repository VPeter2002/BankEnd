using AD41HN_HFT_2022231.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AD41HN_HFT_2022231.Logic.Interfaces
{
    public interface IOhioGlucoseLogic
    {
        void Create(OhioGlucose item);
        void Delete(int id);
        IEnumerable<OhioGlucose> ReadAll();
      
        void Update(OhioGlucose item);
    }
}