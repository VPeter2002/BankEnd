using AD41HN_HFT_2022231.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AD41HN_HFT_2022231.Logic.Interfaces
{
    public interface IPlayerLogic
    {
        void Create(Player item);
        void Delete(int id);
        IEnumerable GetPlayersOnThisPost(string post);
        IEnumerable GetTeamName(string Playername);
        IEnumerable GetTeamId(string Playername);
        IEnumerable<Player> GetGKs();
        IEnumerable<Player> GetHun();
        IEnumerable<Player> GetEng();

        Player Read(int id);
        IEnumerable<Player> ReadAll();
        void Update(Player item);
    }
}