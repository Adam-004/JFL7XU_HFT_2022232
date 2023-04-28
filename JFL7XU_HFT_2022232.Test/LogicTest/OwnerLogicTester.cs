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
    public class OwnerLogicTester
    {
        OwnerLogic logic;
        Mock<IRepository<Owner>> moqOwnerRepo;
        [SetUp]
        public void Init()
        {
            moqOwnerRepo = new();
            moqOwnerRepo.Setup(mhr => mhr.ReadAll()).Returns(new List<Owner>()
                {
                    new Owner(1,"Yoda mester",507),
                    new Owner(2,"Din Djarin",48),
                    new Owner(3,"Bahets",21),
                    new Owner(4,"Béla",32)
                }.AsQueryable());
            moqOwnerRepo.Setup(mhr => mhr.Read(1)).Returns(new Owner(1, "Yoda mester", 507));
            moqOwnerRepo.Setup(mhr => mhr.Read(2)).Returns(new Owner(2, "Din Djarin", 48));
            moqOwnerRepo.Setup(mhr => mhr.Read(3)).Returns(new Owner(3, "Bahets", 21));
            moqOwnerRepo.Setup(mhr => mhr.Read(4)).Returns(new Owner(4, "Béla", 32));
            logic = new OwnerLogic(moqOwnerRepo.Object);
        }

        //Create tests
        [Test]
        public void CreateOwnerTest_Correct()
        {
            var owner = new Owner(5, "Feri", 20);

            //ACT
            logic.Create(owner);
            //ASSERT
            moqOwnerRepo.Verify(r => r.Create(owner), Times.Once);
        }
        [Test]
        public void CreateOwnerTest_NoName()
        {
            var owner = new Owner(5, "", 20);

            //ACT
            try
            {
                logic.Create(owner);
            }
            catch { }
            //ASSERT
            moqOwnerRepo.Verify(r => r.Create(owner), Times.Never);
        }
        [Test]
        public void CreateOwnerTest_IDExists()
        {
            var owner = new Owner(3, "Feri", 20);

            //ACT
            try
            {
                logic.Create(owner);
            }
            catch { }
            //ASSERT
            moqOwnerRepo.Verify(r => r.Create(owner), Times.Never);
        }

        //Read tests
        [Test]
        public void ReadOwnerTest_Correct()
        {
            int ID = 3;
            //ACT
            logic.Read(ID);
            //ASSERT
            moqOwnerRepo.Verify(r => r.Read(ID), Times.Once);
        }
        [Test]
        public void ReadOwnerTest_Exception()
        {
            int ID = 6;
            //ACT
            //ASSERT
            Assert.That(() => logic.Read(ID), Throws.TypeOf<ArgumentException>());
        }

        //Update tests
        [Test]
        public void UpdateOwnerTest_Correct()
        {
            var owner = new Owner(3, "Feri", 20);
            //ACT
            logic.Update(owner);
            //ASSERT
            moqOwnerRepo.Verify(r => r.Update(owner), Times.Once);
        }
        [Test]
        public void UpdateOwnerTest_InCorrect()
        {
            var owner = new Owner(5, "Feri", 20);
            //ACT
            try
            {
                logic.Update(owner);
            }
            catch { }
            //ASSERT
            moqOwnerRepo.Verify(r => r.Update(owner), Times.Never);
        }
    }
}
