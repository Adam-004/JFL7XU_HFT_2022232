using JFL7XU_HFT_2022232.Logic.Interfaces;
using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Logic.Logics
{
    public class HangarLogic : IHangarLogic
    {
        IRepository<Hangar> repo;
        public HangarLogic(IRepository<Hangar> repo)
        {
            this.repo = repo;
        }
        public void Create(Hangar item)
        {
            if (repo.Read(item.ID) is not null)
            {
                throw new ArgumentException("ID already exists!");
            }

            else if (item.Name is null || item.Name == "")
            {
                throw new ArgumentException("Name field was empty!");
            }
            else if (item.Location is null || item.Location == "")
            {
                throw new ArgumentException("Location field was empty!");
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
        public Hangar Read(int id)
        {
            var hangar = repo.Read(id);
            if (hangar is null)
            {
                throw new ArgumentException("Hangar ID is not valid!");
            }
            return hangar;
        }
        public IEnumerable<Hangar> ReadAll()
        {
            return repo.ReadAll();
        }
        public void Update(Hangar item)
        {
            if (repo.Read(item.ID) is null)
            {
                throw new ArgumentException("ID is not valid!");
            }
            repo.Update(item);
        }
    }
}
