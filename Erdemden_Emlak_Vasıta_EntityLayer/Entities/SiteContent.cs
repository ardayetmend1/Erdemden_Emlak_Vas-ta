using System;
using System.ComponentModel.DataAnnotations;

namespace Erdemden_Emlak_Vasıta_EntityLayer
{
    public class SiteContent
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Key { get; set; }

        [MaxLength(500)]
        public string Image { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
