using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Erdemden_Emlak_Vasıta_EntityLayer
{
    public class Model
    {
        public int Id { get; set; }

        public int BrandId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
