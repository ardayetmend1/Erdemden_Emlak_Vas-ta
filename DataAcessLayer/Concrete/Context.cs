using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.Concrete
{
    public class Context : DbContext
    {
        // Constructor Injection - Program.cs üzerinden ayar alabilmesi için
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        // OnConfiguring'i kaldırdık veya sadece fallback olarak bırakabiliriz.
        // Artık ayarları Program.cs yapacak.

        public DbSet<Listing> Listings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<BodyType> BodyTypes { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }
        public DbSet<HousingType> HousingTypes { get; set; }
        public DbSet<ListingImage> ListingImages { get; set; }
        public DbSet<ExpertReport> ExpertReports { get; set; }
        public DbSet<NotaryDocument> NotaryDocuments { get; set; }
        public DbSet<UserFavorite> UserFavorites { get; set; }
        public DbSet<QuoteRequest> QuoteRequests { get; set; }
        public DbSet<SiteContent> SiteContents { get; set; }
    }
}
