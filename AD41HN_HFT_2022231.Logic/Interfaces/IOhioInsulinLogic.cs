using AD41HN_HFT_2022231.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AD41HN_HFT_2022231.Logic.Interfaces
{
    public interface IOhioInsulinLogic
    {
        void Create(OhioInsulin item);
        void Delete(int id);
        OhioInsulin Read(int id);
        IEnumerable<OhioInsulin> ReadAll();
        IEnumerable<OhioInsulin> ReadById(int id);


        void Update(OhioInsulin item);
    }
}