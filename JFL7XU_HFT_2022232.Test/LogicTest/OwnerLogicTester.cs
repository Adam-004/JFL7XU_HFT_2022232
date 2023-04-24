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
    public class OwnerLogicTester
    {
        [TestFixture]
        public class HangarLogicTester
        {
            OwnerLogic logic;
            Mock<IRepository<Owner>> moqHangarRepo;
            [SetUp]
            public void Init()
            {
                moqHangarRepo = new();
                moqHangarRepo.Setup(mhr => mhr.ReadAll()).Returns(new List<Owner>()
                {
                    new Owner(1,"Yoda mester",507),
                    new Owner(2,"Din Djarin",48),
                    new Owner(3,"Bahets",21),
                    new Owner(4,"Béla",32)
                }.AsQueryable());
                moqHangarRepo.Setup(mhr => mhr.Read(1)).Returns(new Owner(1, "Yoda mester", 507));
                moqHangarRepo.Setup(mhr => mhr.Read(2)).Returns(new Owner(2, "Din Djarin", 48));
                moqHangarRepo.Setup(mhr => mhr.Read(3)).Returns(new Owner(3, "Bahets", 21));
                moqHangarRepo.Setup(mhr => mhr.Read(4)).Returns(new Owner(4, "Béla", 32));
                logic = new OwnerLogic(moqHangarRepo.Object);
            }

            //Create tests
            [Test]
            public void CreateHangarTest_Correct()
            {
                var owner = new Owner(5, "Feri", 20);

                //ACT
                logic.Create(owner);
                //ASSERT
                moqHangarRepo.Verify(r => r.Create(owner), Times.Once);
            }
            [Test]
            public void CreateHangarTest_NoName()
            {
                var owner = new Owner(5, "", 20);

                //ACT
                try
                {
                    logic.Create(owner);
                }
                catch { }
                //ASSERT
                moqHangarRepo.Verify(r => r.Create(owner), Times.Never);
            }
            [Test]
            public void CreateHangarTest_IDExists()
            {
                var owner = new Owner(3, "Feri", 20);

                //ACT
                try
                {
                    logic.Create(owner);
                }
                catch { }
                //ASSERT
                moqHangarRepo.Verify(r => r.Create(owner), Times.Never);
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
                Assert.That(() => logic.Read(ID), Throws.TypeOf<NoOwnerFoundWithGivenIdException>());
            }

            //Update tests
            [Test]
            public void UpdateHangarTest_Correct()
            {
                var owner = new Owner(3, "Feri", 20);
                //ACT
                logic.Update(owner);
                //ASSERT
                moqHangarRepo.Verify(r => r.Update(owner), Times.Once);
            }
            [Test]
            public void UpdateHangarTest_InCorrect()
            {
                var owner = new Owner(5, "Feri", 20);
                //ACT
                try
                {
                    logic.Update(owner);
                }
                catch { }
                //ASSERT
                moqHangarRepo.Verify(r => r.Update(owner), Times.Never);
            }
        }
    }
}
