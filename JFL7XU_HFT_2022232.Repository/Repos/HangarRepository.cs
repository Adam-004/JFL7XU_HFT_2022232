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
    public class HangarRepository : GenericRepository<Hangar>,IRepository<Hangar>
    {
        public HangarRepository(SpacecraftOwnershipDBContext ctx) : base(ctx)
        {
        }
        public override Hangar Read(int id)
        {
            return (ctx as SpacecraftOwnershipDBContext).Hangars.FirstOrDefault(h => h.ID == id);
        }
        public override void Update(Hangar item)
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
