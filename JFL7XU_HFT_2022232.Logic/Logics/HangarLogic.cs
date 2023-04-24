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
            repo.Create(item);
        }
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        public Hangar Read(int id)
        {
            return repo.Read(id);
        }
        public IEnumerable<Hangar> ReadAll()
        {
            return repo.ReadAll();
        }
        public void Update(Hangar item)
        {
            repo.Update(item);
        }
    }
}
