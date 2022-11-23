
using AD41HN_HFT_2022231.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Db
{
    public class FWCDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Trainer> Trainers { get; set; }

        public FWCDbContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            if (!builder.IsConfigured)
            {
                //builder.UseInMemoryDatabase("db");
                builder.UseSqlServer(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =|DataDirectory|\Database1.mdf; Integrated Security = True");
                                       
                builder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var players = new Player(1, "Lajos") { TeamId=1};
            var teams = new Team("Magyar") { PlayerId = 1, TrainerId = 1 , Id=1};
            var trainers = new Trainer(1, "István", "Magyar");

            builder.Entity<Player>().HasData(players);
            builder.Entity<Team>().HasData(teams);
            builder.Entity<Trainer>().HasData(trainers);

            builder.Entity<Player>()
                            .HasOne(t => t.Team)
                            .WithMany(p=> p.Players)
                            .HasForeignKey(p=>p.TeamId);



            builder.Entity<Team>(t => t
                            .HasOne(t => t.Trainer)
                            .WithOne(t => t.team)
                            .HasForeignKey<Team>(t=>t.TrainerId))
                            ;
            

        }

    }
}
