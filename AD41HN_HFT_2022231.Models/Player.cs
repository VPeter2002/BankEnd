using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD41HN_HFT_2022231.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual Team Team { get; set; }
        public int TeamId { get; set; }
        public string Post { get; set; }

        public Player(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
