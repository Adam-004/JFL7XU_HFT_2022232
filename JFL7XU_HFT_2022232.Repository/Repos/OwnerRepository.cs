using JFL7XU_HFT_2022232.Repository.Interfaces;
using JFL7XU_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFL7XU_HFT_2022232.Repository.Database;

namespace JFL7XU_HFT_2022232.Repository.Repos
{
    class OwnerRepository : GenericRepository<Owner>, IRepository<Owner>
    {
        public OwnerRepository(SpacecraftOwnershipDBContext ctx) : base(ctx)
        {
        }
        public override Owner Read(int id)
        {
            return ctx.Owners.FirstOrDefault(o => o.ID == id);
        }
        public override void Update(Owner item)
        {
            var old = Read(item.ID);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
