using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.Repository.Database;
using JFL7XU_HFT_2022232.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Repository.Repos
{
    public class StarshipRepository : GenericRepository<Starship>, IRepository<Starship>
    {
        public StarshipRepository(SpacecraftOwnershipDBContext ctx) : base(ctx)
        {
        }
        public override Starship Read(int id)
        {
            return ctx.Starships.FirstOrDefault(s => s.ID == id);
        }
        public override void Update(Starship item)
        {
            var old = Read(item.ID);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
