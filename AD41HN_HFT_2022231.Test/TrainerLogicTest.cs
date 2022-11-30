using AD41HN_HFT_2022231.Logic.Classes;
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
    public class TrainerLogicTest
    {
        TrainerLogic trainerLogic;
        Mock<IRepository<Trainer>> mockTrainerRepo;

        [SetUp]
        public void Init()
        {
            mockTrainerRepo = new Mock<IRepository<Trainer>>();
            mockTrainerRepo.Setup(m => m.ReadAll()).Returns(new List<Trainer>()
            {
               new Trainer (1, "Marco Rossi", "Magyar") {Team = new Team(){Name="Magyar" } },
                new Trainer (2, "Hans-Dieter Flick", "Német") {Team = new Team() {Name="Német"}},
                new Trainer (3, "Gareth Southgate", "Angol"){Team = new Team(){Name="Angol" } },
                new Trainer (4, "Roberto Mancini", "Olasz"){Team = new Team(){Name="Olasz" } },
            }.AsQueryable());
            trainerLogic = new TrainerLogic(mockTrainerRepo.Object);
        }

        // non-croud
        

        //croud
        [Test]
        public void CreateTest()
        {
            Trainer trainer = new Trainer() { Name="Ernő"};
            trainerLogic.Create(trainer);
            mockTrainerRepo.Verify(x => x.Create(trainer), Times.Once);
        }

        [Test]
        public void CreateTestWithNoResult()
        {
            Trainer trainer =new Trainer() { Name="A"};
            try
            {
                trainerLogic.Create(trainer);
            }
            catch
            {

            }
            mockTrainerRepo.Verify(x => x.Create(trainer), Times.Never);
        }
        [Test]
        public void GetTeamOfTrainer_idTest()
        {
            var result = trainerLogic.GetTeamOfTrainer_id(2);
            Assert.IsNotNull(result);
        }
    }
}
