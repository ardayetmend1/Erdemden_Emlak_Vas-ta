namespace EntityLayer.Entities;

public class UserFavorite
{
    public Guid UserId { get; set; }
    public Guid ListingId { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    public virtual User User { get; set; } = null!;
    public virtual Listing Listing { get; set; } = null!;
}
