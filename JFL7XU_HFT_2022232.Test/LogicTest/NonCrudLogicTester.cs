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
    public class NonCrudLogicTester
    {
        StarshipLogic StarshipLogic;
        OwnerLogic OwnerLogic;
        HangarLogic HangarLogic;
        NonCrudLogic NonCrudLogic;

        Mock<IRepository<Starship>> moqShipRepo;
        Mock<IRepository<Owner>> moqOwnerRepo;
        Mock<IRepository<Hangar>> moqHangarRepo;

        [SetUp]
        public void Init()
        {
            List<Starship> s1 = new List<Starship>() { new Starship(1, "Borotvaél", 100, 20271, 3, 2) };
            List<Starship> s2 = new List<Starship>() { new Starship(2, "Nabui Csillagvadász", 60, 20150, 4, 2)};
            List<Starship> s3 = new List<Starship>() { new Starship(3, "Scorpion", 65, 2970, 4, 3) };
            List<Starship> s4 = new List<Starship>() { new Starship(4, "890 Jump", 270, 2998, 4, 3) };

            var h1 = new Hangar(1, "Trade leage platform", "Nabu", 1);
            var h2 = new Hangar(2, "Imperial Landing Pod", "Nevarró", 2);
            var h3 = new Hangar(3, "Self-Land", "Orison", 3);
            var h4 = new Hangar(4, "Kenedy Launch Station", "Houston", 4);

            //Starship
            moqShipRepo = new();
            moqShipRepo.Setup(mhr => mhr.ReadAll()).Returns(new List<Starship>()
            {
                new Starship(1,"Borotvaél",100,20271,3,2),
                new Starship(2,"Nabui Csillagvadász",60,20150,4,2),
                new Starship(3,"Scorpion",65,2970,4,3),
                new Starship(4,"890 Jump",270,2998,4,3)
            }.AsQueryable());
            moqShipRepo.Setup(mhr => mhr.Read(1)).Returns(new Starship(1, "Borotvaél", 100, 20271, 3, 1));
            moqShipRepo.Setup(mhr => mhr.Read(2)).Returns(new Starship(2, "Nabui Csillagvadász", 60, 20150, 4, 2));
            moqShipRepo.Setup(mhr => mhr.Read(3)).Returns(new Starship(3, "Scorpion", 65, 2970, 4, 3));
            moqShipRepo.Setup(mhr => mhr.Read(4)).Returns(new Starship(4, "890 Jump", 270, 2998, 4, 4));
            StarshipLogic = new StarshipLogic(moqShipRepo.Object);

            //Owner
            moqOwnerRepo = new();
            moqOwnerRepo.Setup(mhr => mhr.ReadAll()).Returns(new List<Owner>()
                {
                    new Owner(1, "Yoda mester", 507) { Hangar = h1, Ships=s1},
                    new Owner(2, "Din Djarin", 48) { Hangar = h2, Ships = s2 },
                    new Owner(3, "Bahets", 21) { Hangar = h3, Ships = s3 },
                    new Owner(4, "Béla", 32) { Hangar = h4, Ships = s4 }
                }.AsQueryable());
            moqOwnerRepo.Setup(mhr => mhr.Read(1)).Returns(new Owner(1, "Yoda mester", 507) { Hangar = h1, Ships=s1});
            moqOwnerRepo.Setup(mhr => mhr.Read(2)).Returns(new Owner(2, "Din Djarin", 48) { Hangar = h2, Ships = s2 });
            moqOwnerRepo.Setup(mhr => mhr.Read(3)).Returns(new Owner(3, "Bahets", 21) { Hangar = h3, Ships = s3 });
            moqOwnerRepo.Setup(mhr => mhr.Read(4)).Returns(new Owner(4, "Béla", 32) { Hangar = h4, Ships = s4 });
            OwnerLogic = new OwnerLogic(moqOwnerRepo.Object);

            //Hangar
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
            HangarLogic = new HangarLogic(moqHangarRepo.Object);

            //NonCrud
            NonCrudLogic = new(moqHangarRepo.Object, moqOwnerRepo.Object,moqShipRepo.Object);
        }

        [Test]
        public void ListShips_WhichBuiltAfterTest()
        {
            NonCrudLogic.ListShips_WhichBuiltAfter(20);
            moqShipRepo.Verify(r => r.ReadAll(),Times.Once);
        }
        [Test]
        public void ListHangars_WithShipsMoreThanTest()
        {
            NonCrudLogic.ListHangars_WithShipsMoreThan(2);
            moqHangarRepo.Verify(r => r.ReadAll(), Times.Once);
        }
        [Test]
        public void ListHangars_WithShipsLessThanTest()
        {
            NonCrudLogic.ListHangars_WithShipsLessThan(2);
            moqHangarRepo.Verify(r => r.ReadAll(), Times.Once);
        }
        [Test]
        public void ListOwners_OlderThanTest()
        {
            NonCrudLogic.ListOwners_OlderThan(70);
            moqOwnerRepo.Verify(r => r.ReadAll(), Times.Once);
        }
        [Test]
        public void ListOwners_YoungerThanTest()
        {
            NonCrudLogic.ListOwners_YoungerThan(25);
            moqOwnerRepo.Verify(r => r.ReadAll(), Times.Once);
        }
        [Test]
        public void ListOwners_YoungerAndHasMoreShipsThanTest()
        {
            NonCrudLogic.ListOwners_YoungerAndHasMoreShipsThan(25,2);
            moqOwnerRepo.Verify(r => r.ReadAll(), Times.Once);
        }
        [Test]
        public void ListStatisticsTest()
        {
            NonCrudLogic.ListStatistics();
            moqOwnerRepo.Verify(r => r.ReadAll(), Times.Once);
        }
    }
}
