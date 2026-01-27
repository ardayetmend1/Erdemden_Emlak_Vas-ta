namespace EntityLayer.Entities;

public class UserFavorite
{
    public int UserId { get; set; }
    public int ListingId { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    public virtual User User { get; set; } = null!;
    public virtual Listing Listing { get; set; } = null!;
}
