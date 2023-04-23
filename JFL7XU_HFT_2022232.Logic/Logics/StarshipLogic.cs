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
    public class StarshipLogic : IStarshipLogic
    {
        IRepository<Starship> repo;
        public StarshipLogic(IRepository<Starship> repo)
        {
            this.repo = repo;
        }

        public void Create(Starship item)
        {
            repo.Create(item);
        }
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        public Starship Read(int id)
        {
            return repo.Read(id);
        }
        public IEnumerable<Starship> ReadAll()
        {
            return repo.ReadAll();
        }
        public void Update(Starship item)
        {
            repo.Update(item);
        }
        //Non-CRUDs

    }
}
