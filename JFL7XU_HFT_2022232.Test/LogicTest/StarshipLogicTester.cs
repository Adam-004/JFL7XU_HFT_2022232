using JFL7XU_HFT_2022232.Logic.Logics;
using JFL7XU_HFT_2022232.Models.Exceptions;
using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.Repository.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Test.LogicTest
{
    [TestFixture]
    public class StarshipLogicTester
    {
        StarshipLogic logic;
        Mock<IRepository<Starship>> moqShipRepo;
        [SetUp]
        public void Init()
        {
            moqShipRepo = new();
            moqShipRepo.Setup(mhr => mhr.ReadAll()).Returns(new List<Starship>()
            {
                new Starship(1,"Borotvaél",100,20271,3,2),
                new Starship(2,"Nabui Csillagvadász",60,20150,4,2),
                new Starship(3,"Scorpion",65,2970,4,3),
                new Starship(4,"890 Jump",270,2998,4,3)
            }.AsQueryable());
            moqShipRepo.Setup(mhr => mhr.Read(1)).Returns(new Starship(1, "Borotvaél", 100, 20271, 3, 2));
            moqShipRepo.Setup(mhr => mhr.Read(2)).Returns(new Starship(2, "Nabui Csillagvadász", 60, 20150, 4, 2));
            moqShipRepo.Setup(mhr => mhr.Read(3)).Returns(new Starship(3, "Scorpion", 65, 2970, 4, 3));
            moqShipRepo.Setup(mhr => mhr.Read(4)).Returns(new Starship(4, "890 Jump", 270, 2998, 4, 3));
            logic = new StarshipLogic(moqShipRepo.Object);
        }

        //Create tests
        [Test]
        public void CreateStarshipTest_Correct()
        {
            var ship = new Starship(5, "Idris", 405, 2870, 2, 3);

            //ACT
            logic.Create(ship);
            //ASSERT
            moqShipRepo.Verify(r => r.Create(ship), Times.Once);
        }
        [Test]
        public void CreateStarshipTest_NoName()
        {
            var ship = new Starship(5, "", 405, 2870, 2, 3);

            //ACT
            try
            {
                logic.Create(ship);
            }
            catch { }
            //ASSERT
            moqShipRepo.Verify(r => r.Create(ship), Times.Never);
        }
        [Test]
        public void CreateStarshipTest_IDExists()
        {
            var ship = new Starship(5, "", 405, 2870, 2, 3);

            //ACT
            try
            {
                logic.Create(ship);
            }
            catch { }
            //ASSERT
            moqShipRepo.Verify(r => r.Create(ship), Times.Never);
        }

        //Read tests
        [Test]
        public void ReadStarshipTest_Correct()
        {
            int ID = 3;
            //ACT
            logic.Read(ID);
            //ASSERT
            moqShipRepo.Verify(r => r.Read(ID), Times.Once);
        }
        [Test]
        public void ReadStarshipTest_Exception()
        {
            int ID = 6;
            //ACT
            //ASSERT
            Assert.That(() => logic.Read(ID), Throws.TypeOf<NoStarshipFoundWithGivenIdException>());
        }

        //Update tests
        [Test]
        public void UpdateStarshipTest_Correct()
        {
            var ship = new Starship(4, "", 405, 2870, 2, 3);
            //ACT
            logic.Update(ship);
            //ASSERT
            moqShipRepo.Verify(r => r.Update(ship), Times.Once);
        }
        [Test]
        public void UpdateStarshipTest_InCorrect()
        {
            var ship = new Starship(5, "", 405, 2870, 2, 3);
            //ACT
            try
            {
                logic.Update(ship);
            }
            catch { }
            //ASSERT
            moqShipRepo.Verify(r => r.Update(ship), Times.Never);
        }
    }
}
