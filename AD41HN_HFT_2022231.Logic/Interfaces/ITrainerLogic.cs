using AD41HN_HFT_2022231.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AD41HN_HFT_2022231.Logic.Interfaces
{
    public interface ITrainerLogic
    {
        void Create(Trainer item);
        void Delete(int id);
        IEnumerable<Team> GetTeamOfTrainer_id(int id);
        Trainer Read(int id);
        IEnumerable<Trainer> ReadAll();
        void Update(Trainer item);
    }
}