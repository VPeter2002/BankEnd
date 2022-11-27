using AD41HN_HFT_2022231.Models;
using System.Linq;

namespace AD41HN_HFT_2022231.Logic.Interfaces
{
    public interface ITrainerLogic
    {
        void Create(Trainer item);
        void Delete(int id);
        IQueryable GetPlayersOfTrainer_id(int id);
        Trainer Read(int id);
        IQueryable<Trainer> ReadAll();
        void Update(Trainer item);
    }
}