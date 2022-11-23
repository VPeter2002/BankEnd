using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Models
{
    public class Team
    {

        public int PlayerId { get; set; }
        public int TrainerId { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public List<Player> Players { get; set; }
        public Team(string name)
        {
            Name = name;
            Players = new List<Player>();
        }
    }
}
