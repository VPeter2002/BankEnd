
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Xml.Serialization;

    namespace AD41HN_HFT_2022231.Models
    {
    [XmlRoot("patient")]
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DbId { get; set; } // EF kulcs

        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("weight")]
        public double Weight { get; set; }

        [XmlAttribute("insulin_type")]
        public string InsulinType { get; set; }

        
        [XmlElement("glucose_level")]
        public virtual GlucoseLevel GlucoseLevel { get; set; }

        
        [XmlElement("finger_stick")]
        public virtual FingerStick FingerStick { get; set; }

        
        [XmlElement("basal")]
        public virtual Basal Basal { get; set; }

        
        [XmlElement("temp_basal")]
        public virtual TempBasal TempBasal { get; set; }

       
        [XmlElement("bolus")]
        public virtual Bolus Bolus { get; set; }

        
        [XmlElement("meal")]
        public virtual MealSection Meal { get; set; }

        
        [XmlElement("sleep")]
        public virtual Sleep Sleep { get; set; }

        
        [XmlElement("work")]
        public virtual Work Work { get; set; }

        
        [XmlElement("hypo_event")]
        public virtual HypoEvent HypoEvent { get; set; }

       
        [XmlElement("illness")]
        public virtual Illness Illness { get; set; }

       
        [XmlElement("exercise")]
        public virtual Exercise Exercise { get; set; }

       
        [XmlElement("basis_heart_rate")]
        public virtual BasisSensorSection BasisHeartRate { get; set; }

      
        [XmlElement("basis_gsr")]
        public virtual BasisSensorSection BasisGsr { get; set; }

       
        [XmlElement("basis_skin_temperature")]
        public virtual BasisSensorSection BasisSkinTemp { get; set; }

       
        [XmlElement("basis_air_temperature")]
        public virtual BasisSensorSection BasisAirTemp { get; set; }

        
        [XmlElement("basis_steps")]
        public virtual BasisSensorSection BasisSteps { get; set; }

        
        [XmlElement("basis_sleep")]
        public virtual BasisSleep BasisSleep { get; set; }
    }

    public class GlucoseLevel
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DbId { get; set; } // EF kulcs
        [NotMapped]
            [XmlElement("event")]
        public virtual List<TimestampedValue> Events { get; set; }
        }

        public class FingerStick
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DbId { get; set; } // EF kulcs
        [NotMapped]
            [XmlElement("event")]
        public virtual List<TimestampedValue> Events { get; set; }
        }

        public class Basal
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DbId { get; set; } // EF kulcs
        [NotMapped]
            [XmlElement("event")]
        public virtual List<TimestampedValue> Events { get; set; }
        }

        public class TempBasal
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DbId { get; set; } // EF kulcs
        [NotMapped]
            [XmlElement("event")]
        public virtual List<TimestampedPeriodValue> Events { get; set; }
        }

        public class Bolus
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DbId { get; set; } // EF kulcs
        [NotMapped]
            [XmlElement("event")]
        public virtual List<BolusEvent> Events { get; set; }
        }

        public class MealSection
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DbId { get; set; } // EF kulcs
        [NotMapped]
            [XmlElement("event")]
        public virtual List<MealEvent> Events { get; set; }
        }

        public class Sleep
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DbId { get; set; } // EF kulcs
        [NotMapped]
            [XmlElement("event")]
        public virtual List<SleepEvent> Events { get; set; }
        }

        public class Work
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DbId { get; set; } // EF kulcs
        [NotMapped]
            [XmlElement("event")]
        public virtual List<WorkEvent> Events { get; set; }
        }

        public class HypoEvent
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DbId { get; set; } // EF kulcs
        [NotMapped]
            [XmlElement("event")]
        public virtual List<HypoEventEntry> Events { get; set; }
        }

        public class Illness
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DbId { get; set; } // EF kulcs
        [NotMapped]
            [XmlElement("event")]
        public virtual List<IllnessEvent> Events { get; set; }
        }

        public class Exercise
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DbId { get; set; } // EF kulcs
        [NotMapped]
            [XmlElement("event")]
        public virtual List<ExerciseEvent> Events { get; set; }
        }

        public class BasisSensorSection
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DbId { get; set; } // EF kulcs
        [NotMapped]
            [XmlElement("event")]
        public virtual List<TimestampedValue> Events { get; set; }
        }

        public class BasisSleep
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DbId { get; set; } // EF kulcs
        [NotMapped]
            [XmlElement("event")]
        public virtual List<BasisSleepEvent> Events { get; set; }
        }

        public class TimestampedValue
        {
            [NotMapped]
            [XmlAttribute("ts")]
        public virtual string Timestamp { get; set; }

            [NotMapped]
            [XmlAttribute("value")]
        public virtual string Value { get; set; }
        }

        public class TimestampedPeriodValue
        {
            [NotMapped]
            [XmlAttribute("ts_begin")]
        public string Start { get; set; }

            [NotMapped]
            [XmlAttribute("ts_end")]
        public string End { get; set; }

            [NotMapped]
            [XmlAttribute("value")]
        public string Value { get; set; }
        }

        public class BolusEvent
        {
            [NotMapped]
            [XmlAttribute("ts_begin")]
        public string Start { get; set; }

            [NotMapped]
            [XmlAttribute("ts_end")]
        public string End { get; set; }

            [NotMapped]
            [XmlAttribute("type")]
        public string Type { get; set; }

            [NotMapped]
            [XmlAttribute("dose")]
        public double Dose { get; set; }

            [NotMapped]
            [XmlAttribute("bwz_carb_input")]
        public int CarbInput { get; set; }
        }

        public class MealEvent
        {
            [NotMapped]
            [XmlAttribute("ts")]
        public string Timestamp { get; set; }

            [NotMapped]
            [XmlAttribute("type")]
        public string Type { get; set; }

            [NotMapped]
            [XmlAttribute("carbs")]
        public int Carbs { get; set; }
        }

        public class SleepEvent
        {
            [NotMapped]
            [XmlAttribute("ts_begin")]
        public string Start { get; set; }

            [NotMapped]
            [XmlAttribute("ts_end")]
        public string End { get; set; }

            [NotMapped]
            [XmlAttribute("quality")]
        public string Quality { get; set; }
        }

        public class WorkEvent
        {
            [NotMapped]
            [XmlAttribute("ts_begin")]
        public string Start { get; set; }

            [NotMapped]
            [XmlAttribute("ts_end")]
        public string End { get; set; }

            [NotMapped]
            [XmlAttribute("intensity")]
        public int Intensity { get; set; }
        }

        public class HypoEventEntry
        {
            [NotMapped]
            [XmlAttribute("ts")]
        public string Timestamp { get; set; }

            [NotMapped]
            [XmlElement("symptom")]
        public virtual Symptom Symptom { get; set; }
        }

        public  class Symptom
        {
            [NotMapped]
            [XmlAttribute("name")]
        public string Name { get; set; }
        }

        public class IllnessEvent
        {
            [NotMapped]
            [XmlAttribute("ts_begin")]
        public string Start { get; set; }

            [NotMapped]
            [XmlAttribute("ts_end")]
        public string End { get; set; }

            [NotMapped]
            [XmlAttribute("type")]
        public string Type { get; set; }

            [NotMapped]
            [XmlAttribute("description")]
        public string Description { get; set; }
        }

        public class ExerciseEvent
        {
            [NotMapped]
            [XmlAttribute("ts")]
        public string Timestamp { get; set; }

            [NotMapped]
            [XmlAttribute("intensity")]
        public int Intensity { get; set; }

            [NotMapped]
            [XmlAttribute("type")]
        public string Type { get; set; }

            [NotMapped]
            [XmlAttribute("duration")]
        public int Duration { get; set; }

            [NotMapped]
            [XmlAttribute("competitive")]
        public string Competitive { get; set; }
        }

        public class BasisSleepEvent
        {
            [NotMapped]
            [XmlAttribute("tbegin")]
        public string Begin { get; set; }

            [NotMapped]
            [XmlAttribute("tend")]
        public string End { get; set; }

            [NotMapped]
            [XmlAttribute("quality")]
        public string Quality { get; set; }

            [NotMapped]
            [XmlAttribute("type")]
        public string Type { get; set; }
        }
    }


