using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Erdemden_Emlak_Vasıta_EntityLayer
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<District> Districts { get; set; } = new List<District>();
        public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();
    }
}
