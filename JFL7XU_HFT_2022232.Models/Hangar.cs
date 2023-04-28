using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Models
{
    public class Hangar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Location { get; set; }
        public int OwnerID { get; set; }

        [JsonIgnore]
        public virtual Owner Owner { get; set; }
        public Hangar()
        {
        }
        public Hangar(int id, string name, string location, int ownerID)
        {
            Id = id;
            Name = name;
            Location = location;
            OwnerID = ownerID;
        }

        public override string ToString()
        {
            return $"{Id}. {Name}, loacated at {Location}.";
        }
    }
}
