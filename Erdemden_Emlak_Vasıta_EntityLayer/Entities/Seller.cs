using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Erdemden_Emlak_Vasıta_EntityLayer
{
    public class Seller
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(500)]
        public string Avatar { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();
    }
}
