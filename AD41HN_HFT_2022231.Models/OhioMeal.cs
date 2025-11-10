using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AD41HN_HFT_2022231.Models
{
    public class OhioMeal
    {

       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public long Timestamp { get; set; }

        public string Type { get; set; }

        public int PatID { get; set; }
        [Key]
        public string Key { get; set; }

        public long M { get; set; }

        public string Collection { get; set; }

        public virtual List<FoodItem> Foods { get; set; } = new();

        [NotMapped]
        public DateTime DateTime => DateTimeOffset.FromUnixTimeSeconds(Timestamp).DateTime;


        public OhioMeal()
        {

        }
    }
}
