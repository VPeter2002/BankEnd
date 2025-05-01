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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Repository
{
    public class FWCDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Trainer> Trainers { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<CareSensAirData> CareSensAirDatas { get; set; }


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
            var players = new Player[]
            {
                new Player() {Id=1, Name="Németh Kristof", TeamId = 1, Post="FW"},
                new Player() {Id=2, Name="Attila Szalai", TeamId = 1, Post="DF"},
                new Player() {Id=3, Name="Dominik Szoboszla", TeamId = 1, Post="MF"},
                new Player() {Id=4, Name="Willi Orbán", TeamId = 1, Post="DF"},
                new Player() {Id=5, Name="Roland Sallai", TeamId = 1, Post="FW"},
                new Player() {Id=6, Name="Ádám Nagy", TeamId = 1, Post="MF"},
                new Player() {Id=7, Name="Thomas Müller", TeamId = 2, Post="MF"},
                new Player() {Id=8, Name="Manuel Neuer", TeamId = 2, Post="GK"},
                new Player() {Id=9, Name="Thomas Müller", TeamId = 2, Post="DF"},
                new Player() {Id=10, Name="Timo Werner", TeamId = 2, Post="FW"},
                new Player() {Id=11, Name="Joshua Kimmich", TeamId = 2, Post="MD"},
                new Player() {Id=12, Name="Emre Can", TeamId = 2, Post="MF"},
                new Player() {Id=13, Name="Antonio Rüdiger", TeamId = 2, Post="DF"},
                new Player() {Id=14, Name="Jordan Pickford", TeamId = 3, Post="MF"},
                new Player() {Id=15, Name="Nick Pope", TeamId = 3, Post="FW"},
                new Player() {Id=16, Name="Luke Shaw", TeamId = 3, Post="MF"},
                new Player() {Id=17, Name="Jack Grealish", TeamId = 3, Post="DF"},
                new Player() {Id=18, Name="Jordan Pickford", TeamId = 3, Post="FW"},
                new Player() {Id=19, Name="Gianluigi Donnarumma", TeamId = 4, Post="GK"},
                new Player() {Id=21, Name="Giovanni Di Lorenzo", TeamId = 4, Post="FW"},
                new Player() {Id=22, Name="Samuele Ricci", TeamId = 4, Post="DF"},
                new Player() {Id=23, Name="Simone Pafundi", TeamId = 4, Post="FW"},
            };


            var teams = new Team[]
            {
                new Team ("Magyar") { TrainerId = 1 , Id=1},
                new Team ("Német") { TrainerId = 2 , Id=2},
                new Team ("Angol") { TrainerId = 3 , Id=3},
                new Team ("Olasz") { TrainerId = 4 , Id=4}
            };
            var trainers = new Trainer[]
            {
                new Trainer (1, "Marco Rossi", "Magyar"),
                new Trainer (2, "Hans-Dieter Flick", "Német"),
                new Trainer (3, "Gareth Southgate", "Angol"),
                new Trainer (4, "Roberto Mancini", "Olasz")
            };


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

            // -------------------------------------- caresensairdatas

            //using (TextFieldParser parser = new TextFieldParser("C:\\Users\\Peti ROG\\Desktop\\Tanulós\\Diabetes Webapplication Backend\\CareSensAirDatas.csv"))
            //{
            //    parser.TextFieldType = FieldType.Delimited;
            //    parser.SetDelimiters(",");
            //    List<CareSensAirData> CareSensAirDatadatas = new List<CareSensAirData>();
            //    double trend = 0.00;

            //    while (!parser.EndOfData)
            //    {
            //        //Processing row
            //        string[] row = parser.ReadFields();
                    

            //            if (row[6] == "")
            //            {
            //                CareSensAirDatadatas.Add(new CareSensAirData
            //                {
            //                    Device = row[0],
            //                    SerialNumber = row[1],
            //                    Numberof_Value =int.Parse(row[2]),
            //                    Date_Time = DateTime.Parse( row[3]),
            //                    Value = double.Parse(row[4], CultureInfo.InvariantCulture),
            //                    Unit = row[5],
            //                    Trend_Rate = 0.00
            //                });

            //            }
            //            else
            //            {
            //                CareSensAirDatadatas.Add(new CareSensAirData
            //                {

            //                    Device = row[0],
            //                    SerialNumber = row[1],
            //                    Numberof_Value = int.Parse(row[2]),
            //                    Date_Time = DateTime.Parse(row[3]),
            //                    Value = double.Parse(row[4], CultureInfo.InvariantCulture),
            //                    Unit = row[5],
            //                    Trend_Rate = double.Parse(row[6], CultureInfo.InvariantCulture)
            //                });
            //            }
                    
            //    }
            //   // builder.Entity<CareSensAirData>().HasData(CareSensAirDatadatas);

            //}





            builder.Entity<Player>().HasData(players);
            builder.Entity<Team>().HasData(teams);
            builder.Entity<Trainer>().HasData(trainers);

            builder.Entity<Player>()
                            .HasOne(p => p.Team)
                            .WithMany(t => t.Players)
                            .HasForeignKey(p => p.TeamId);

            builder.Entity<Team>(t => t
                            .HasOne(t => t.Trainer)
                            .WithOne(t => t.Team)
                            .HasForeignKey<Team>(t => t.TrainerId));


        }

    }

    
    
}
