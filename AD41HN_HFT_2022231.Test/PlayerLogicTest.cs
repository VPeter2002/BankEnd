using AD41HN_HFT_2022231.Logic.Classes;
using AD41HN_HFT_2022231.Logic.Interfaces;
using AD41HN_HFT_2022231.Models;
using AD41HN_HFT_2022231.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Test
{
    [TestFixture]

    public class PlayerLogicTest
    {
        PlayerLogic playerLogic;
        Mock<IRepository<Player>> mockPlayerRepo;

        [SetUp]
        public void Init()
        {
            mockPlayerRepo = new Mock<IRepository<Player>>();
            mockPlayerRepo.Setup(m => m.ReadAll()).Returns(new List<Player>()
            {
                new Player() {Id=10, Name="Timo Werner", TeamId = 2, Post="FW", Team= new Team("Német"){Trainer = new Trainer(){Name="Sanyi" } } },
                new Player() {Id=11, Name="Joshua Kimmich", TeamId = 2, Post="MD", Team= new Team("Német")},
                new Player() {Id=12, Name="Emre Can", TeamId = 2, Post="MF", Team= new Team("Német")},
                new Player() {Id=13, Name="Antonio Rüdiger", TeamId = 2, Post="DF", Team= new Team("Német")},
                new Player() {Id=14, Name="Jordan Pickford", TeamId = 3, Post="MF", Team= new Team("Angol")},
                new Player() {Id=15, Name="Nick Pope", TeamId = 3, Post="FW", Team= new Team("Angol")}
            }.AsQueryable());
            playerLogic = new PlayerLogic(mockPlayerRepo.Object);
        }

        [Test]
        public void CreateTest()
        {
            Player team = new Player() {Name="asd", TeamId=2 };
            playerLogic.Create(team);
            mockPlayerRepo.Verify(x => x.Create(team), Times.Once);
        }
        [Test]
        public void CreateTestWithNoResult()
        {
            Player team = new Player() { Name = "asd", TeamId = -2 };

            try
            {
                playerLogic.Create(team);

            }
            catch
            {


            }
            mockPlayerRepo.Verify(x => x.Create(team), Times.Never);

        }

        // non-croud
        [Test]
        public void GetTeamNameTest()
        {
            var result = playerLogic.GetTeamName("Jordan Pickford");
            Assert.IsNotNull(result);
        }
        //[Test]
        //public void GetTrainerNameTest()
        //{
        //   Player Sanyi = new Player() { Id = 10, Name = "Timo Werner", TeamId = 2, Post = "FW", Team = new Team("Német") { Trainer = new Trainer() { Name = "Sanyi" } } };

        //    var result = playerLogic.GetTrainerName(Sanyi);
        //    Assert.IsNotNull(result);

        //}



    }
}
