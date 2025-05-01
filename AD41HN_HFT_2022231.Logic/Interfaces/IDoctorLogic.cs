using AD41HN_HFT_2022231.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AD41HN_HFT_2022231.Logic.Interfaces
{
    public interface IDoctorLogic
    {
        void Create(Doctor item);
        void Delete(int id);
        Player Read(int id);
        IEnumerable<Doctor> ReadAll();
        void Update(Doctor item);
    }
}