
using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.SeedData
{
    public static class SeedFuelTypes
    {
        public static async Task SeedAsync(Context context)
        {
            if (await context.Set<FuelType>().AnyAsync())
                return;

            var fuelTypes = new[] { "Benzin", "Dizel", "LPG", "Hibrit", "Elektrik" };

            foreach (var fuelTypeName in fuelTypes)
            {
                var fuelType = new FuelType
                {
                    Id = Guid.NewGuid(),
                    Name = fuelTypeName,
                    CreatedAt = DateTime.UtcNow
                };

                context.Set<FuelType>().Add(fuelType);
            }

            await context.SaveChangesAsync();
        }
    }
}
