using AD41HN_HFT_2022231.Models;
using System.Linq;

namespace AD41HN_HFT_2022231.Logic.Classes
{
    public interface ITeamLogic
    {
        void Create(Team item);
        void Delete(int id);
        IQueryable GetGoalKeepersInTeam(string teamname);
        Team Read(int id);
        IQueryable<Team> ReadAll();
        void Update(Team item);
    }
}