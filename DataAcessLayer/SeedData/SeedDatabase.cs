using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.SeedData
{
    public static class SeedDatabase
    {
        public static async Task InitializeAsync(Context context)
        {
            // Lookup verilerini seed et (sıralama önemli - VehicleTypes önce olmalı çünkü BodyTypes'a bağlı)
            await SeedVehicleTypes.SeedAsync(context);
            await SeedVehicleTypes.EnsureBodyTypesAsync(context); // Eksik BodyType kayıtlarını oluştur
            await SeedBrands.SeedAsync(context);
            await SeedFuelTypes.SeedAsync(context);
            await SeedTransmissionTypes.SeedAsync(context);
            await SeedCities.SeedAsync(context);
            await SeedHousingTypes.SeedAsync(context);

            // Mevcut markalara eksik modelleri ekle
            await SeedBrands.AddMissingModelsAsync(context);

            // Paketleri seed et (Brands/Models seed edildikten sonra çalışmalı)
            await SeedPackages.SeedAsync(context);

            // Mevcut modellere eksik paketleri ekle
            await SeedPackages.AddMissingPackagesAsync(context);

            // EN SON: Tüm modellere doğru BodyTypeId ata (AddMissingModels sonrası yeni modeller de düzelsin)
            await SeedBrands.UpdateModelBodyTypesAsync(context);

            // Admin kullanıcı oluştur
            await SeedAdminUser(context);
        }

        private static async Task SeedAdminUser(Context context)
        {
            // Admin var mı kontrol et
            if (!await context.Users.AnyAsync(u => u.Role == UserRole.Admin))
            {
                var adminUser = new User
                {
                    Name = "Sistem Yöneticisi",
                    Email = "admin@erdemden.com",
                    PasswordHash = HashPassword("Admin123!"),
                    Role = UserRole.Admin,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                context.Users.Add(adminUser);
                await context.SaveChangesAsync();
            }
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
