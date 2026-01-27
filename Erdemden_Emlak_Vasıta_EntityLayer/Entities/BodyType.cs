using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Erdemden_Emlak_Vasıta_EntityLayer
{
    public class BodyType
    {
        public int Id { get; set; }

        public int VehicleTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual VehicleType VehicleType { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
