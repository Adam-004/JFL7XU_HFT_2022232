using JFL7XU_HFT_2022232.Logic.Interfaces;
using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.Models.Exceptions;
using JFL7XU_HFT_2022232.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Logic.Logics
{
    public class OwnerLogic : IOwnerLogic
    {
        IRepository<Owner> repo;
        public OwnerLogic(IRepository<Owner> repo)
        {
            this.repo = repo;
        }

        public void Create(Owner item)
        {
            if (repo.Read(item.ID) is not null)
            {
                throw new GivenIDAlreadyExistsException();
            }
            else if (item.Name is null)
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
        public Owner Read(int id)
        {
            var owner = repo.Read(id);
            if (owner is null)
            {
                throw new NoOwnerFoundWithGivenIdException();
            }
            return owner;
        }
        public IEnumerable<Owner> ReadAll()
        {
            return repo.ReadAll();
        }
        public void Update(Owner item)
        {
            if (repo.Read(item.ID) is not null)
            {
                throw new GivenIDNotFoundException();
            }
            repo.Update(item);
        }
    }
}
