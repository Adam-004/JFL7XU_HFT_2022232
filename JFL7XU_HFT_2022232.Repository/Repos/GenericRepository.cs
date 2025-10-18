using JFL7XU_HFT_2022232.Repository.Database;
using JFL7XU_HFT_2022232.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Repository.Repos
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected DbContext ctx;
        public GenericRepository(DbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Create(T item)
        {
            ctx.Set<T>().Add(item);
            ctx.SaveChanges();
        }
        public virtual IQueryable<T> ReadAll()
        {
            return ctx.Set<T>();
        }
        public virtual void Delete(int id)
        {
            ctx.Set<T>().Remove(Read(id));
            ctx.SaveChanges();
        }
        public abstract T Read(int id);
        public abstract void Update(T item);
    }
}
