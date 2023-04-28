using JFL7XU_HFT_2022232.Logic.Logics;
using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.Models.Exceptions;
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
    public class HangarLogicTester
    {
        HangarLogic logic;
        Mock<IRepository<Hangar>> moqHangarRepo;
        [SetUp]
        public void Init()
        {
            moqHangarRepo = new();
            moqHangarRepo.Setup(mhr => mhr.ReadAll()).Returns(new List<Hangar>()
            {
                new Hangar(1,"Trade leage platform","Nabu",1),
                new Hangar(2,"Imperial Landing Pod","Nevarró",2),
                new Hangar(3,"Self-Land","Orison",3),
                new Hangar(4,"Kenedy Launch Station","Houston",4),
            }.AsQueryable());
            moqHangarRepo.Setup(mhr => mhr.Read(1)).Returns(new Hangar(1, "Trade leage platform", "Nabu", 1));
            moqHangarRepo.Setup(mhr => mhr.Read(2)).Returns(new Hangar(2, "Imperial Landing Pod", "Nevarró", 2));
            moqHangarRepo.Setup(mhr => mhr.Read(3)).Returns(new Hangar(3, "Self-Land", "Orison", 3));
            moqHangarRepo.Setup(mhr => mhr.Read(4)).Returns(new Hangar(4, "Kenedy Launch Station", "Houston", 4));
            logic = new HangarLogic(moqHangarRepo.Object);
        }

        //Create tests
        [Test]
        public void CreateHangarTest_Correct()
        {
            var hangar = new Hangar(5, "Falcon Launch Station", "Cape Canaveral", 5);

            //ACT
            logic.Create(hangar);
            //ASSERT
            moqHangarRepo.Verify(r => r.Create(hangar), Times.Once);
        }
        [Test]
        public void CreateHangarTest_NoName()
        {
            var hangar = new Hangar(5, "", "Cape Canaveral", 5);

            //ACT
            try
            {
                logic.Create(hangar);
            }
            catch{}
            //ASSERT
            moqHangarRepo.Verify(r => r.Create(hangar), Times.Never);
        }
        [Test]
        public void CreateHangarTest_NoLocation()
        {
            var hangar = new Hangar(5, "Falcon Launch Station", "", 5);

            //ACT
            try
            {
                logic.Create(hangar);
            }
            catch { }
            //ASSERT
            moqHangarRepo.Verify(r => r.Create(hangar), Times.Never);
        }
        [Test]
        public void CreateHangarTest_IDExists()
        {
            var hangar = new Hangar(3, "Falcon Launch Station", "Cape Canaveral", 5);

            //ACT
            try
            {
                logic.Create(hangar);
            }
            catch { }
            //ASSERT
            moqHangarRepo.Verify(r => r.Create(hangar), Times.Never);
        }

        //Read tests
        [Test]
        public void ReadHangarTest_Correct()
        {
            int ID = 3;
            //ACT
            logic.Read(ID);
            //ASSERT
            moqHangarRepo.Verify(r => r.Read(ID), Times.Once);
        }
        [Test]
        public void ReadHangarTest_Exception()
        {
            int ID = 6;
            //ACT
            //ASSERT
            Assert.Throws<ArgumentException>(() => _ = logic.Read(ID));
        }

        //Update tests
        [Test]
        public void UpdateHangarTest_Correct()
        {
            var hangar = new Hangar(3, "Falcon Launch Station", "Cape Canaveral", 5);
            //ACT
            logic.Update(hangar);
            //ASSERT
            moqHangarRepo.Verify(r => r.Update(hangar), Times.Once);
        }
        [Test]
        public void UpdateHangarTest_InCorrect()
        {
            var hangar = new Hangar(5, "Falcon Launch Station", "Cape Canaveral", 5);
            //ACT
            try
            {
                logic.Update(hangar);
            }
            catch{}
            //ASSERT
            moqHangarRepo.Verify(r => r.Update(hangar), Times.Never);
        }
    }
}
