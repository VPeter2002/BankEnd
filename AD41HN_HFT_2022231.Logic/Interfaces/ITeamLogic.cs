using AD41HN_HFT_2022231.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AD41HN_HFT_2022231.Logic.Interfaces
{
    public interface ITeamLogic
    {
        void Create(Team item);
        void Delete(int id);
        IEnumerable GetGoalKeepersInTeam(string teamname);
        Team Read(int id);
        IEnumerable<Team> ReadAll();
        void Update(Team item);
    }
}