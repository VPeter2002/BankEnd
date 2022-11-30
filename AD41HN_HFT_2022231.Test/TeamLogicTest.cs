using AD41HN_HFT_2022231.Logic.Classes;
using AD41HN_HFT_2022231.Models;
using AD41HN_HFT_2022231.Repository;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AD41HN_HFT_2022231.Test
{
    [TestFixture]
    
    public class TeamLogicTest
    {
        TeamLogic teamLogic;
        Mock<IRepository<Team>> mockTeamRepo;

        [SetUp]
        public void Init()
        {
            mockTeamRepo = new Mock<IRepository<Team>>();
            mockTeamRepo.Setup(m => m.ReadAll()).Returns(new List<Team>()
            {
                new Team("Magyar"){Id=1,TrainerId=1, 
                    Players={ new Player() {Id=1, Name="Németh Kristof", TeamId = 1, Post="GK"},
                        new Player() {Id=2, Name="Attila Szalai", TeamId = 1, Post="DF"},
                        new Player() {Id=3, Name="Dominik Szoboszla", TeamId = 1, Post="MF"},
                        new Player() {Id=6, Name="Ádám Nagy", TeamId = 1, Post="MF"} } },

                        new Team("Spanyol"){Id=2,TrainerId=2,
                        Players={ new Player() {Id=1, Name="Németh Kristof", TeamId = 1, Post="MF"},
                        new Player() {Id=2, Name="Attila Szalai", TeamId = 1, Post="DF"},
                        new Player() {Id=3, Name="Dominik Szoboszla", TeamId = 1, Post="MF"} } },

                        new Team("Olasz"){Id=3,TrainerId=3},
            }.AsQueryable()) ;
            teamLogic = new TeamLogic(mockTeamRepo.Object);
        }

        // non-croud
        [Test]
        public void GetGoalKeepersInTeamTest()
        {
            var result = teamLogic.GetGoalKeepersInTeam("Magyar");
            Assert.IsNotNull(result);
        }
        [Test]
        public void GetGoalKeepersInTeamTestWithNull()
        {
            var result = teamLogic.GetGoalKeepersInTeam("Spanyol");
            Assert.IsEmpty(result);
        }

        //croud
        


    }
}
