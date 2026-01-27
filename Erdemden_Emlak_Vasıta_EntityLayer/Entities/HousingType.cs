using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Erdemden_Emlak_Vasıta_EntityLayer
{
    public class HousingType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<RealEstate> RealEstates { get; set; } = new List<RealEstate>();
    }
}
