using JFL7XU_HFT_2022232.Logic.Interfaces;
using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.Models.Exceptions;
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
            if (repo.Read(item.Id) is not null)
            {
                throw new GivenIDAlreadyExistsException();
            }

            else if (item.Name is null)
            {
                throw new NameWasEmptyException();
            }
            else if (item.Location is null)
            {
                throw new LocationWasEmptyException();
            }
            repo.Create(item);
        }
        public void Delete(int id)
        {
            if (repo.Read(id) is not null)
            {
                throw new GivenIDNotFoundException();
            }
            repo.Delete(id);
        }
        public Hangar Read(int id)
        {
            var hangar = repo.Read(id);
            if (hangar is null)
            {
                throw new NoHangarFoundWithGivenIdException();
            }
            return hangar;
        }
        public IEnumerable<Hangar> ReadAll()
        {
            return repo.ReadAll();
        }
        public void Update(Hangar item)
        {
            if (repo.Read(item.Id) is not null)
            {
                throw new GivenIDNotFoundException();
            }
            repo.Update(item);
        }
    }
}
