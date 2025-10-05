using JFL7XU_HFT_2022232.Logic.Interfaces;
using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace JFL7XU_HFT_2022232.Logic.Logics
{
    public class NonCrudLogic : INonCrudLogic
    {
        private readonly IRepository<Hangar> HangarRepo;
        private readonly IRepository<Owner> OwnerRepo;
        private readonly IRepository<Starship> StarshipRepo;
        public NonCrudLogic(IRepository<Hangar> hangarRepo, IRepository<Owner> ownerRepo, IRepository<Starship> starshipRepo)
        {
            HangarRepo = hangarRepo;
            OwnerRepo = ownerRepo;
            StarshipRepo = starshipRepo;
        }

        public IEnumerable<Hangar> ListHangars_WithShipsMoreThan(int quantity)
        {
            return HangarRepo.ReadAll().Where(h => h.Owner.Ships.Count > quantity);
        }

        public IEnumerable<Hangar> ListHangars_WithShipsLessThan(int quantity)
        {
            return HangarRepo.ReadAll().Where(h => h.Owner.Ships.Count < quantity);
        }

        public IEnumerable<Owner> ListOwners_OlderThan(int year)
        {
            return OwnerRepo.ReadAll().Where(o => o.Age > year);
        }

        public IEnumerable<Owner> ListOwners_YoungerThan(int year)
        {
            return OwnerRepo.ReadAll().Where(o => o.Age < year);
        }

        public IEnumerable<Owner> ListOwners_YoungerAndHasMoreShipsThan(int year, int quantity)
        {
            return OwnerRepo.ReadAll().Where(o => o.Age < year && o.Ships.Count > quantity);
        }

        public IEnumerable<Starship> ListShips_WhichBuiltAfter(int year)
        {
            return StarshipRepo.ReadAll().Where(ship => ship.YearOfManu > year);
        }

        public IEnumerable<OwnershipStatistics> ListStatistics()
        {
            List<OwnershipStatistics> stats = new List<OwnershipStatistics>();
            Owner owner;
            IEnumerable<Starship> ships;
            Hangar hangar;

            foreach (var item in OwnerRepo.ReadAll())
            {
                owner = item;
                ships = item.Ships;
                hangar = item.Hangar;
                ;
                stats.Add(new OwnershipStatistics(owner, hangar, ships));
            }

            return stats;
        }
    }
}
