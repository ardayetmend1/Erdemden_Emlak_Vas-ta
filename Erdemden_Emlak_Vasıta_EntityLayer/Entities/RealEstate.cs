using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erdemden_Emlak_Vasıta_EntityLayer
{
    public class RealEstate
    {
        [Key]
        [ForeignKey("Listing")]
        public int ListingId { get; set; }

        // Lookup FK
        public int HousingTypeId { get; set; }

        // Diğer alanlar
        [MaxLength(20)]
        public string RoomCount { get; set; }

        public int? Size { get; set; }
        public int? Floor { get; set; }
        public int? TotalFloors { get; set; }
        public int? BuildingAge { get; set; }
        public bool? HasElevator { get; set; }
        public bool? HasParking { get; set; }
        public bool? IsFurnished { get; set; }

        // Navigation Properties
        public virtual Listing Listing { get; set; }
        public virtual HousingType HousingType { get; set; }
    }
}
