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
    public class PlayerLogic : IPlayerLogic
    {
        IRepository<Player> repo;

        public PlayerLogic(IRepository<Player> repo)
        {
            this.repo = repo;
        }

        public void Create(Player item)
        {
            if (item.TeamId < 0 )
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

        public IEnumerable<Player> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Player item)
        {
            this.repo.Update(item);
        }

        //non cruds

        public IEnumerable GetPlayersOnThisPost(string post)
        {
            return this.repo
               .ReadAll()
               .Where(t => t.Post == post);
        }
        public IEnumerable GetTeamName(string Playername)
        {
            return this.repo.ReadAll().Where(t => t.Name.Equals(Playername)).Select(t => t.Team.Name);
        }
        public IEnumerable GetTrainerName(Player Playername)
        {
            if (Playername.Team==null)
            {
                    throw new ArgumentException(" has no team");
            }
            return this.repo.ReadAll().Where(t => t.Name.Equals(Playername)).Select(t => t.Team.Trainer.Name);
        }








    }
}

