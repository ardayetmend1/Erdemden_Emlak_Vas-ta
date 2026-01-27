using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Erdemden_Emlak_Vasıta_EntityLayer
{
    public class District
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();
    }
}
