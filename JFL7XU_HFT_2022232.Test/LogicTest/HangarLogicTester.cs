using JFL7XU_HFT_2022232.Logic.Logics;
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
    }
}
