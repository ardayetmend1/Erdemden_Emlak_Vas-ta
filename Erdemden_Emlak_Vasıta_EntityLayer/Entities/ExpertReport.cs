using System;
using System.ComponentModel.DataAnnotations;

namespace Erdemden_Emlak_Vasıta_EntityLayer
{
    public class ExpertReport
    {
        public int Id { get; set; }

        public int QuoteRequestId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string ContentType { get; set; }

        [Required]
        public byte[] Data { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public virtual QuoteRequest QuoteRequest { get; set; }
    }
}
