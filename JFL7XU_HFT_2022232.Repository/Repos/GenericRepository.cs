using JFL7XU_HFT_2022232.Repository.Database;
using JFL7XU_HFT_2022232.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Repository.Repos
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected SpacecraftOwnershipDBContext ctx;
        public GenericRepository(SpacecraftOwnershipDBContext ctx)
        {
            this.ctx = ctx;
        }

        public void Create(T item)
        {
            ctx.Set<T>().Add(item);
            ctx.SaveChanges();
        }
        public IQueryable<T> ReadAll()
        {
            return ctx.Set<T>();
        }
        public void Delete(int id)
        {
            ctx.Set<T>().Remove(Read(id));
            ctx.SaveChanges();
        }
        public abstract T Read(int id);
        public abstract void Update(T item);
    }
}
