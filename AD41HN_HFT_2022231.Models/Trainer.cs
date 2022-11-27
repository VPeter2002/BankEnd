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
        [Required]
        public string TeamName { get; set; }
        public virtual Team Team { get; set; }
        public Trainer()
        {

        }
        public Trainer(int id, string name, string teamName)
        {
            Id = id;
            Name = name;
            TeamName = teamName;
        }
    }
}
