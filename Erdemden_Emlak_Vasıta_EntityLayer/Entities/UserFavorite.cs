using System;

namespace Erdemden_Emlak_Vasıta_EntityLayer
{
    public class UserFavorite
    {
        public int UserId { get; set; }
        public int ListingId { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        public virtual User User { get; set; }
        public virtual Listing Listing { get; set; }
    }
}
