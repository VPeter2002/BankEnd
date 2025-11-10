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
    public class OhioExercise
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public string Key { get; set; }

        public int Intensity { get; set; }

        public string? ExerciseType { get; set; }

        public bool? Competitive { get; set; }

        public long Timestamp { get; set; }

        public long EndTimestamp { get; set; }

        public int PatID { get; set; }

        public string Type { get; set; }

        public long M { get; set; }

        public string Collection { get; set; }

        [NotMapped]
        public DateTime DateTimeStart => DateTimeOffset.FromUnixTimeSeconds(Timestamp).DateTime;
        public DateTime DateTimeEnd => DateTimeOffset.FromUnixTimeSeconds(EndTimestamp).DateTime;


        public OhioExercise()
        {

        }
    }
}
