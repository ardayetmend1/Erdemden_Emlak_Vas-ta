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
            // Veritabanının var olduğundan emin ol
            // await context.Database.MigrateAsync(); // Migration kullanılıyorsa

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
