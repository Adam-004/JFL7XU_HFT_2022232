using JFL7XU_HFT_2022232.Logic.Interfaces;
using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace JFL7XU_HFT_2022232.Logic.Logics
{
    public class StarshipLogic : IStarshipLogic
    {
        private readonly IRepository<Starship> repo;

        public StarshipLogic(IRepository<Starship> repo)
        {
            this.repo = repo;
        }

        public void Create(Starship item)
        {
            if (repo.Read(item.ID) is not null)
            {
                throw new ArgumentException("ID already exists!");
            }
            else if (item.Name is null || item.Name == "")
            {
                throw new ArgumentException("Name field was empty!");
            }
            repo.Create(item);
        }

        public void Delete(int id)
        {
            if (repo.Read(id) is null)
            {
                throw new ArgumentException("ID is not valid!");
            }
            repo.Delete(id);
        }

        public Starship Read(int id)
        {
            var ship = repo.Read(id);
            if (ship is null)
            {
                throw new ArgumentException("Starship ID is not valid!");
            }
            return ship;
        }

        public IEnumerable<Starship> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Starship item)
        {
            if (!item.IsUnInitialized())
            {
                if (repo.Read(item.ID) is null)
                {
                    throw new ArgumentException("ID is not valid!");
                }
                repo.Update(item);
            }
        }
    }
}
