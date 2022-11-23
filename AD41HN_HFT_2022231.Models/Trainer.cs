using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Models
{
    public class Trainer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        [Required]
        public string TeamName { get; set; }
        public virtual Team team { get; set; }
       // public virtual ICollection<Player> Players { get; set; }

        public Trainer(int id, string name, string teamName, int age = 999)
        {
            Id = id;
            Name = name;
            TeamName = teamName;
            Age = age;
        }
    }
}
