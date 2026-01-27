using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Erdemden_Emlak_Vasıta_EntityLayer
{
    public class VehicleType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<BodyType> BodyTypes { get; set; } = new List<BodyType>();
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
