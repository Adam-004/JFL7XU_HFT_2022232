using JFL7XU_HFT_2022232.Logic.Interfaces;
using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace JFL7XU_HFT_2022232.Logic.Logics
{
    public class OwnerLogic : IOwnerLogic
    {
        private readonly IRepository<Owner> repo;

        public OwnerLogic(IRepository<Owner> repo)
        {
            this.repo = repo;
        }

        public void Create(Owner item)
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

        public Owner Read(int id)
        {
            var owner = repo.Read(id);
            if (owner is null)
            {
                throw new ArgumentException("Hangar ID is not valid!");
            }
            return owner;
        }

        public IEnumerable<Owner> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Owner item)
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
