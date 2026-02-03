using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.SeedData
{
    public static class SeedHousingTypes
    {
        public static async Task SeedAsync(Context context)
        {
            if (await context.Set<HousingType>().AnyAsync())
                return;

            var housingTypes = new[] { "Daire", "Villa", "Rezidans", "Yazlık", "Yalı Dairesi", "Müstakil Ev", "Prefabrik" };

            foreach (var housingTypeName in housingTypes)
            {
                var housingType = new HousingType
                {
                    Id = Guid.NewGuid(),
                    Name = housingTypeName,
                    CreatedAt = DateTime.UtcNow
                };

                context.Set<HousingType>().Add(housingType);
            }

            await context.SaveChangesAsync();
        }
    }
}
