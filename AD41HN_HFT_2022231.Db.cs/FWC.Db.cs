
//using AD41HN_HFT_2022231.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Emit;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;

//namespace AD41HN_HFT_2022231.Db
//{
//    public class FWCDbContext : DbContext
//    {
//        public DbSet<Player> Players { get; set; }
//        public DbSet<Team> Teams { get; set; }
//        public DbSet<Trainer> Trainers { get; set; }

//        public FWCDbContext()
//        {
//            this.Database.EnsureCreated();
            
//        }
//        protected override void OnConfiguring(DbContextOptionsBuilder builder)
//        {

//            if (!builder.IsConfigured)
//            {
//                builder.UseInMemoryDatabase("db");
//                //builder.UseSqlServer(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =|DataDirectory|\Database1.mdf; Integrated Security = True");

//                builder.UseLazyLoadingProxies();
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            var players = new Player[]
//            {
//                new Player() {Id=1, Name="Németh Kristof", TeamId = 1, Post="FW"},
//                new Player() {Id=2, Name="Attila Szalai", TeamId = 1, Post="DF"},
//                new Player() {Id=3, Name="Dominik Szoboszla", TeamId = 1, Post="MF"},
//                new Player() {Id=4, Name="Willi Orbán", TeamId = 1, Post="DF"},
//                new Player() {Id=5, Name="Roland Sallai", TeamId = 1, Post="FW"},
//                new Player() {Id=6, Name="Ádám Nagy", TeamId = 1, Post="MF"},
//                new Player() {Id=7, Name="Thomas Müller", TeamId = 2, Post="MF"},
//                new Player() {Id=8, Name="Manuel Neuer", TeamId = 2, Post="GK"},
//                new Player() {Id=9, Name="Thomas Müller", TeamId = 2, Post="DF"},
//                new Player() {Id=10, Name="Timo Werner", TeamId = 2, Post="FW"},
//                new Player() {Id=11, Name="Joshua Kimmich", TeamId = 2, Post="MD"},
//                new Player() {Id=12, Name="Emre Can", TeamId = 2, Post="MF"},
//                new Player() {Id=13, Name="Antonio Rüdiger", TeamId = 2, Post="DF"},
//                new Player() {Id=14, Name="Jordan Pickford", TeamId = 3, Post="MF"},
//                new Player() {Id=15, Name="Nick Pope", TeamId = 3, Post="FW"},
//                new Player() {Id=16, Name="Luke Shaw", TeamId = 3, Post="MF"},
//                new Player() {Id=17, Name="Jack Grealish", TeamId = 3, Post="DF"},
//                new Player() {Id=18, Name="Jordan Pickford", TeamId = 3, Post="FW"},
//                new Player() {Id=19, Name="Gianluigi Donnarumma", TeamId = 4, Post="GK"},
//                new Player() {Id=21, Name="Giovanni Di Lorenzo", TeamId = 4, Post="FW"},
//                new Player() {Id=22, Name="Samuele Ricci", TeamId = 4, Post="DF"},
//                new Player() {Id=23, Name="Simone Pafundi", TeamId = 4, Post="FW"},
//            };


//            var teams = new Team[]
//            {
//                new Team ("Magyar") { TrainerId = 1 , Id=1},
//                new Team ("Német") { TrainerId = 2 , Id=2},
//                new Team ("Angol") { TrainerId = 3 , Id=3},
//                new Team ("Olasz") { TrainerId = 4 , Id=4}
//            };
//            var trainers = new Trainer[]
//            {
//                new Trainer (1, "Marco Rossi", "Magyar"),
//                new Trainer (2, "Hans-Dieter Flick", "Német"),
//                new Trainer (3, "Gareth Southgate", "Angol"),
//                new Trainer (4, "Roberto Mancini", "Olasz")
//            };

//            builder.Entity<Player>().HasData(players);
//            builder.Entity<Team>().HasData(teams);
//            builder.Entity<Trainer>().HasData(trainers);

//            builder.Entity<Player>()
//                            .HasOne(p => p.Team)
//                            .WithMany(t => t.Players)
//                            .HasForeignKey(p => p.TeamId);

//            builder.Entity<Team>(t => t
//                            .HasOne(t => t.Trainer)
//                            .WithOne(t => t.Team)
//                            .HasForeignKey<Team>(t => t.TrainerId));


//        }

//    }
//}
