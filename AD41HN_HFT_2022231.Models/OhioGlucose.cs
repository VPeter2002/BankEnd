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
    public class OhioGlucose
    {

        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double Value { get; set; }
        public int PatID { get; set; }
        public string Type { get; set; }
        [Key]
        public  string Key { get; set; }
        public int M { get; set; }
        public string Collection { get; set; }
        public int TimeStamp { get; set; }
        [NotMapped]
        public DateTime DateTime => DateTimeOffset.FromUnixTimeSeconds(TimeStamp).DateTime;

        

        public OhioGlucose()
        {

        }
    }
}
