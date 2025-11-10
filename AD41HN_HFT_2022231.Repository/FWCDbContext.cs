using AD41HN_HFT_2022231.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Repository
{
    public class FWCDbContext : DbContext
    {
       

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<CareSensAirData> CareSensAirDatas { get; set; }
        public DbSet<OhioGlucose> OhioGlucose { get; set; }
        public DbSet<OhioMeal> OhioMeal { get; set; }
        public DbSet<OhioInsulin> OhioInsulin { get; set; }
        public DbSet<OhioExercise> OhioExercise { get; set; }
        public DbSet<Patient> Patients { get; set; }


        public FWCDbContext()
        {
            this.Database.EnsureCreated();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            if (!builder.IsConfigured)
            {
                builder.UseInMemoryDatabase("db");
                //builder.UseSqlServer(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =|DataDirectory|\Database1.mdf; Integrated Security = True");

                builder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            string jsonFilePath = "C:\\Users\\Peti ROG\\Desktop\\Tanulós\\Diabetes Webapplication Backend\\AD41HN_HFT_2022231.Repository\\Doctors.json"; // A JSON fájl neve

           
            if (File.Exists(jsonFilePath))
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                var doctors = JsonConvert.DeserializeObject<List<Doctor>>(jsonContent);

                builder.Entity<Doctor>().HasData(doctors);

                foreach (var doctor in doctors)
                {
                    Console.WriteLine($"ID: {doctor.Id}, Name: {doctor.Name}, Password: {doctor.Password}");
                }
            }
            else
            {
                Console.WriteLine("A fájl nem található!");
            }
            //builder.Entity<Basal>().HasNoKey();
            //builder.Entity<TempBasal>().HasNoKey();
            //builder.Entity<Bolus>().HasNoKey();
            //builder.Entity<MealSection>().HasNoKey();
            //builder.Entity<Sleep>().HasNoKey();
            //builder.Entity<Work>().HasNoKey();
            //builder.Entity<HypoEvent>().HasNoKey();
            //builder.Entity<Illness>().HasNoKey();
            //builder.Entity<Exercise>().HasNoKey();
            //builder.Entity<BasisSensorSection>().HasNoKey();
            //builder.Entity<BasisSleep>().HasNoKey();
            //builder.Entity<GlucoseLevel>().HasNoKey();
            //builder.Entity<FingerStick>().HasNoKey();
            //builder.Entity<Symptom>().HasNoKey();

            //// Ezen belül lévő eseménytípusokat is ha szükséges
            
            //builder.Entity<BolusEvent>().HasNoKey();
            //builder.Entity<MealEvent>().HasNoKey();
            //builder.Entity<SleepEvent>().HasNoKey();
            //builder.Entity<WorkEvent>().HasNoKey();
            //builder.Entity<HypoEventEntry>().HasNoKey();
            //builder.Entity<ExerciseEvent>().HasNoKey();
           






            builder.Entity<FoodItem>()
            .OwnsOne(f => f.Details);


        }

    }

    
    
}
