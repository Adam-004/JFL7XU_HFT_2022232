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
    public class StarshipLogic : IStarshipLogic
    {
        IRepository<Starship> repo;
        public StarshipLogic(IRepository<Starship> repo)
        {
            this.repo = repo;
        }

        public void Create(Starship item)
        {
            if (repo.Read(item.ID) is not null)
            {
                throw new GivenIDAlreadyExistsException();
            }
            else if (item.Name is null || item.Name == "")
            {
                throw new NameWasEmptyException();
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
        public Starship Read(int id)
        {
            var ship = repo.Read(id);
            if (ship is null)
            {
                throw new NoStarshipFoundWithGivenIdException();
            }
            return ship;
        }
        public IEnumerable<Starship> ReadAll()
        {
            return repo.ReadAll();
        }
        public void Update(Starship item)
        {
            if (repo.Read(item.ID) is null)
            {
                throw new GivenIDNotFoundException();
            }
            repo.Update(item);
        }
    }
}
