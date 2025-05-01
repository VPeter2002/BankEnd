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
    public class CareSensAirData
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Device { get; set; }
        public  string SerialNumber { get; set; }
        public int Numberof_Value { get; set; }
        public DateTime Date_Time { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
        public double? Trend_Rate { get; set; }

        public CareSensAirData()
        {

        }
    }
}
