using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; } 
        public int TrainerId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual Trainer Trainer { get; set; }
        public Team(string name)
        {
            Name = name;
            Players = new List<Player>();
        }
    }
}
