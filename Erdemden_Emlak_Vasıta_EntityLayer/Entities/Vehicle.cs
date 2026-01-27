using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erdemden_Emlak_Vasıta_EntityLayer
{
    public class Vehicle
    {
        [Key]
        [ForeignKey("Listing")]
        public int ListingId { get; set; }

        // Lookup FK'ler
        public int VehicleTypeId { get; set; }
        public int? BodyTypeId { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int FuelTypeId { get; set; }
        public int TransmissionTypeId { get; set; }

        // Diğer alanlar
        public int? Year { get; set; }
        public int? Km { get; set; }

        [MaxLength(50)]
        public string Color { get; set; }

        [MaxLength(255)]
        public string DamageStatus { get; set; }

        // Navigation Properties
        public virtual Listing Listing { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        public virtual BodyType BodyType { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Model Model { get; set; }
        public virtual FuelType FuelType { get; set; }
        public virtual TransmissionType TransmissionType { get; set; }
    }
}
