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
    public enum ShipType
    {
        Transport = 1,
        Fregatte = 2,
        Cruiser = 3,
        Fighter = 4
    }
    public class Starship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public int Size { get; set; } //in tons
        public int YearOfManu { get; set; } //year of manufacture
        [Range(1, 4)]
        public int Type { get; set; }
        public int OwnerID { get; set; }

        [JsonIgnore]
        public virtual Owner Owner { get; set; }

        public Starship()
        {

        }
        public Starship(int id, string name, int size, int yearOfManu, int type, int ownerId)
        {
            if (!(type > 0 && type <= 4))
            {
                throw new ArgumentException("Invalid shiptype!");
            }
            ID = id;
            Name = name;
            Size = size;
            YearOfManu = yearOfManu;
            Type = type;
            OwnerID = ownerId;
        }
        public override string ToString()
        {
            return $"{ID}. {Name}, {(ShipType)Type} class ship, weighs {Size} tons, manufactured in the year of {YearOfManu}.";
        }
    }
}
