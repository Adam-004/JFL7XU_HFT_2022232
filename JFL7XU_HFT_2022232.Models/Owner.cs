using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Models
{
    public class Owner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public int Age { get; set; }

        public virtual ICollection<Starship> Ships { get; set; }
        public virtual Hangar Hangar { get; set; }
        
        public Owner()
        {

        }
        public Owner(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }
}
