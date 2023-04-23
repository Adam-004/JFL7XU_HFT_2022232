using JFL7XU_HFT_2022232.Logic.Interfaces;
using JFL7XU_HFT_2022232.Models;
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
            repo.Create(item);
        }
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        public Owner Read(int id)
        {
            return repo.Read(id);
        }
        public IEnumerable<Owner> ReadAll()
        {
            return repo.ReadAll();
        }
        public void Update(Owner item)
        {
            repo.Update(item);
        }

        //Non-CRUDs

    }
}
