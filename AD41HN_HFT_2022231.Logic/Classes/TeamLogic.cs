using AD41HN_HFT_2022231.Logic.Interfaces;
using AD41HN_HFT_2022231.Models;
using AD41HN_HFT_2022231.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Logic.Classes
{
    public class TeamLogic : ITeamLogic
    {
        IRepository<Team> repo;

        public TeamLogic(IRepository<Team> repo)
        {
            this.repo = repo;
        }

        public void Create(Team item)
        {
            if (item.Name.Length <2)
            {
                throw new ArgumentException("Invalid TeamId...");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Team Read(int id)
        {
            var team = this.repo.Read(id);
            if (team.Name == null)
            {
                throw new ArgumentException("Team not exists");
            }
            return team;

        }

        public IEnumerable<Team> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Team item)
        {
            this.repo.Update(item);
        }

        //non cruds

        public IEnumerable GetGoalKeepersInTeam(string teamname)
        {
            return this.repo
               .ReadAll()
               .Where(t => t.Name == teamname).Select(p => p.Players.Where(p => p.Post == "GK"));
        }

    }
}
