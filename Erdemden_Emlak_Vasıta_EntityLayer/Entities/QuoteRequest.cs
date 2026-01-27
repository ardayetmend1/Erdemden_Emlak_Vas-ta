using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Erdemden_Emlak_Vasıta_EntityLayer
{
    public class QuoteRequest
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Araç bilgisi
        [MaxLength(20)]
        public string Plate { get; set; }

        [MaxLength(100)]
        public string Brand { get; set; }

        [MaxLength(100)]
        public string Model { get; set; }

        [MaxLength(10)]
        public string Year { get; set; }

        [MaxLength(20)]
        public string Km { get; set; }

        [MaxLength(50)]
        public string Gear { get; set; }

        [MaxLength(50)]
        public string Fuel { get; set; }

        [MaxLength(500)]
        public string Damage { get; set; }

        // Müşteri bilgisi
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<ExpertReport> ExpertReports { get; set; } = new List<ExpertReport>();
    }
}
