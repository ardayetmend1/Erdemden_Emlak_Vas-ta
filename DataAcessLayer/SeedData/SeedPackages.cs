using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.SeedData
{
    public static class SeedPackages
    {
        /// <summary>
        /// Tüm modellere ait paketleri seed eder (Türkiye pazarı, son 5 yıl)
        /// </summary>
        public static async Task SeedAsync(Context context)
        {
            if (await context.Set<Package>().AnyAsync())
                return;

            var models = await context.Set<Model>()
                .Include(m => m.Brand)
                .ToListAsync();

            // Model ismine göre paket eşleştirmesi
            var modelPackages = new Dictionary<string, string[]>
            {
                // ==================== BMW ====================
                { "3 Serisi", new[] { "Sport Line", "M Sport", "Luxury Line", "M Sport Pro" } },
                { "5 Serisi", new[] { "Luxury Line", "Executive", "M Sport" } },
                { "X1", new[] { "xLine", "Sport Line", "M Sport" } },
                { "X3", new[] { "xLine", "M Sport", "Exclusive" } },
                { "X5", new[] { "xLine", "M Sport", "M Sport Pro" } },
                { "iX", new[] { "First Edition Essence", "First Edition Sport" } },

                // ==================== Mercedes ====================
                { "C Serisi", new[] { "Avantgarde", "Exclusive", "AMG Line" } },
                { "E Serisi", new[] { "Avantgarde", "Exclusive", "AMG Line" } },
                { "S Serisi", new[] { "AMG Line" } },
                { "CLA", new[] { "Style", "AMG Line" } },
                { "G Serisi", new[] { "AMG Line" } },
                { "EQE", new[] { "Electric Art", "AMG Line" } },
                { "EQS", new[] { "Electric Art", "AMG Line" } },

                // ==================== Audi ====================
                { "A3", new[] { "Advanced", "S Line" } },
                { "A4", new[] { "Advanced", "S Line" } },
                { "A6", new[] { "Advanced", "S Line" } },
                { "Q3", new[] { "Advanced", "S Line" } },
                { "Q5", new[] { "Advanced", "S Line" } },
                { "Q7", new[] { "Advanced", "S Line" } },
                { "Q8", new[] { "Advanced", "S Line" } },
                { "e-tron", new[] { "Advanced", "S Line" } },

                // ==================== Volkswagen ====================
                { "Golf", new[] { "Impression", "Life", "Style", "R-Line" } },
                { "Polo", new[] { "Impression", "Life", "Style", "R-Line" } },
                { "Passat", new[] { "Impression", "Elegance", "R-Line" } },
                { "Tiguan", new[] { "Life", "Elegance", "R-Line" } },
                { "Touareg", new[] { "Elegance", "R-Line" } },
                { "Amarok", new[] { "Life", "Style", "PanAmericana", "Aventura" } },

                // ==================== Toyota ====================
                { "Corolla", new[] { "Vision", "Dream", "Flame", "Passion" } },
                { "Yaris", new[] { "Vision", "Dream", "Flame" } },
                { "C-HR", new[] { "Flame", "Passion" } },
                { "RAV4", new[] { "Flame", "Passion" } },
                { "Hilux", new[] { "Hi-Cruiser", "Adventure", "Invincible" } },
                { "Land Cruiser", new[] { "Standart" } },

                // ==================== Volvo ====================
                { "S60", new[] { "Core", "Plus", "Ultra" } },
                { "S90", new[] { "Plus", "Ultra" } },
                { "XC40", new[] { "Core", "Plus", "Ultra" } },
                { "XC60", new[] { "Core", "Plus", "Ultra" } },
                { "XC90", new[] { "Plus", "Ultra" } },

                // ==================== Tesla ====================
                { "Model 3", new[] { "Standard", "Long Range", "Performance" } },
                { "Model Y", new[] { "Standard", "Long Range", "Performance" } },
                { "Model S", new[] { "Long Range", "Plaid" } },
                { "Model X", new[] { "Long Range", "Plaid" } },

                // ==================== Honda ====================
                { "Civic", new[] { "Elegance", "Executive", "Sport" } },
                { "City", new[] { "Elegance", "Executive" } },
                { "HR-V", new[] { "Elegance", "Advance", "Style" } },
                { "CR-V", new[] { "Elegance", "Executive" } },

                // ==================== Ford ====================
                { "Focus", new[] { "Trend X", "Titanium", "ST-Line" } },
                { "Fiesta", new[] { "Titanium", "ST-Line" } },
                { "Puma", new[] { "Titanium", "ST-Line" } },
                { "Kuga", new[] { "Titanium", "ST-Line", "Vignale" } },
                { "Ranger", new[] { "XLT", "Wildtrak", "Raptor" } },

                // ==================== Hyundai ====================
                { "i10", new[] { "Jump", "Style", "Elite" } },
                { "i20", new[] { "Jump", "Style", "Elite" } },
                { "Elantra", new[] { "Prime", "Elite" } },
                { "Tucson", new[] { "Comfort", "Prime", "Elite" } },
                { "Bayon", new[] { "Jump", "Style", "Elite" } },
                { "IONIQ 5", new[] { "Progressive", "Advance" } }
            };

            foreach (var model in models)
            {
                if (modelPackages.TryGetValue(model.Name, out var packages))
                {
                    foreach (var packageName in packages)
                    {
                        var package = new Package
                        {
                            Id = Guid.NewGuid(),
                            ModelId = model.Id,
                            Name = packageName,
                            CreatedAt = DateTime.UtcNow
                        };

                        context.Set<Package>().Add(package);
                    }
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
