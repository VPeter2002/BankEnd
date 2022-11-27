using AD41HN_HFT_2022231.Models;
using System.Linq;

namespace AD41HN_HFT_2022231.Logic.Interfaces
{
    public interface IPlayerLogic
    {
        void Create(Player item);
        void Delete(int id);
        IQueryable GetPlayersOnThisPost(string post);
        IQueryable GetTeamName(string Playername);
        IQueryable GetTrainerName(int Playername);
        Player Read(int id);
        IQueryable<Player> ReadAll();
        void Update(Player item);
    }
}