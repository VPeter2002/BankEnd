using AD41HN_HFT_2022231.Models;
using AD41HN_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Logic.Classes
{
    public class PlayerLogic
    {
        IRepository<Player> repo;

        public PlayerLogic(IRepository<Player> repo)
        {
            this.repo = repo;
        }

        public void Create(Player item)
        {
            if (item.TeamId > 4)
            {
                throw new ArgumentException("Invalid TeamId...");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Player Read(int id)
        {
            var player = this.repo.Read(id);
            if (player == null)
            {
                throw new ArgumentException("Player not exists");
            }
            return player;

        }

        public IQueryable<Player> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Player item)
        {
            this.repo.Update(item);
        }

        //non cruds

        public IQueryable GetPlayersOnThisPost(string post)
        {
            return this.repo
               .ReadAll()
               .Where(t => t.Post == post);
        }
        public IQueryable GetTeamName(int Playername)
        {
            return this.repo.ReadAll().Where(t => t.Name.Equals(Playername)).Select(t=>t.Team.Name);
        }
        public IQueryable GetTrainerName(int Playername)
        {
            return this.repo.ReadAll().Where(t => t.Name.Equals(Playername)).Select(t=>t.Team.Trainer.Name);
        }
        


       

        


    }
}

