using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.SeedData
{
    public static class SeedHousingTypes
    {
        public static async Task SeedAsync(Context context)
        {
            var dbSet = context.Set<HousingType>();
            var existingNames = await dbSet.Select(h => h.Name).ToListAsync();

            var seedData = new (string Name, RealEstateCategory Category)[]
            {
                // ===== KONUT =====
                ("Daire", RealEstateCategory.Konut),
                ("Villa", RealEstateCategory.Konut),
                ("Rezidans", RealEstateCategory.Konut),
                ("Yazlık", RealEstateCategory.Konut),
                ("Yalı Dairesi", RealEstateCategory.Konut),
                ("Müstakil Ev", RealEstateCategory.Konut),
                ("Prefabrik", RealEstateCategory.Konut),

                // ===== İŞ YERİ =====
                ("Dükkan", RealEstateCategory.IsYeri),
                ("Mağaza", RealEstateCategory.IsYeri),
                ("Ofis", RealEstateCategory.IsYeri),
                ("Plaza", RealEstateCategory.IsYeri),
                ("İş Hanı Katı", RealEstateCategory.IsYeri),
                ("Depo", RealEstateCategory.IsYeri),
                ("Atölye", RealEstateCategory.IsYeri),
                ("Fabrika", RealEstateCategory.IsYeri),
                ("Showroom", RealEstateCategory.IsYeri),
                ("Kafe & Restaurant", RealEstateCategory.IsYeri),
                ("Otel & Pansiyon", RealEstateCategory.IsYeri),
                ("Düğün Salonu", RealEstateCategory.IsYeri),
                ("Çiftlik", RealEstateCategory.IsYeri),

                // ===== ARSA =====
                ("Konut Arsası", RealEstateCategory.Arsa),
                ("Ticari Arsa", RealEstateCategory.Arsa),
                ("Sanayi Arsası", RealEstateCategory.Arsa),
                ("Turizm Arsası", RealEstateCategory.Arsa),
                ("Karma İmar Arsası", RealEstateCategory.Arsa),
                ("Tarım Arsası", RealEstateCategory.Arsa),
                ("Tarla", RealEstateCategory.Arsa),
                ("Bağ & Bahçe", RealEstateCategory.Arsa)
            };

            var toAdd = seedData
                .Where(s => !existingNames.Contains(s.Name))
                .Select(s => new HousingType
                {
                    Id = Guid.NewGuid(),
                    Name = s.Name,
                    Category = s.Category,
                    CreatedAt = DateTime.UtcNow
                })
                .ToList();

            if (toAdd.Count > 0)
            {
                dbSet.AddRange(toAdd);
                await context.SaveChangesAsync();
            }
        }
    }
}
