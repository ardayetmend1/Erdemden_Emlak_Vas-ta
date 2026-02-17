using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.SeedData
{
    public static class SeedVehicleTypes
    {
        public static async Task SeedAsync(Context context)
        {
            if (await context.Set<VehicleType>().AnyAsync())
                return;

            var vehicleTypeBodyMap = new Dictionary<string, string[]>
            {
                { "Otomobil", new[] { "Sedan", "Hatchback", "Station Wagon", "Coupe", "Cabrio", "Roadster" } },
                { "SUV & Arazi Araçları", new[] { "SUV", "Pickup", "Crossover", "Arazi Aracı" } }
            };

            foreach (var vehicleTypeData in vehicleTypeBodyMap)
            {
                var vehicleType = new VehicleType
                {
                    Id = Guid.NewGuid(),
                    Name = vehicleTypeData.Key,
                    CreatedAt = DateTime.UtcNow
                };

                context.Set<VehicleType>().Add(vehicleType);

                foreach (var bodyTypeName in vehicleTypeData.Value)
                {
                    var bodyType = new BodyType
                    {
                        Id = Guid.NewGuid(),
                        VehicleTypeId = vehicleType.Id,
                        Name = bodyTypeName,
                        CreatedAt = DateTime.UtcNow
                    };

                    context.Set<BodyType>().Add(bodyType);
                }
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Eksik BodyType kayıtlarını oluşturur (VehicleTypes zaten varsa SeedAsync atlar, bu metot eksikleri tamamlar)
        /// </summary>
        public static async Task EnsureBodyTypesAsync(Context context)
        {
            var vehicleTypes = await context.Set<VehicleType>().ToListAsync();
            var existingBodyTypes = await context.Set<BodyType>().ToListAsync();

            var requiredBodyTypes = new Dictionary<string, string[]>
            {
                { "Otomobil", new[] { "Sedan", "Hatchback", "Station Wagon", "Coupe", "Cabrio", "Roadster" } },
                { "SUV & Arazi Araçları", new[] { "SUV", "Pickup", "Crossover", "Arazi Aracı" } }
            };

            var added = false;

            foreach (var entry in requiredBodyTypes)
            {
                var vehicleType = vehicleTypes.FirstOrDefault(vt => vt.Name == entry.Key);
                if (vehicleType == null) continue;

                foreach (var bodyTypeName in entry.Value)
                {
                    var exists = existingBodyTypes.Any(bt => bt.Name == bodyTypeName && bt.VehicleTypeId == vehicleType.Id);
                    if (exists) continue;

                    context.Set<BodyType>().Add(new BodyType
                    {
                        Id = Guid.NewGuid(),
                        VehicleTypeId = vehicleType.Id,
                        Name = bodyTypeName,
                        CreatedAt = DateTime.UtcNow
                    });

                    added = true;
                }
            }

            if (added)
                await context.SaveChangesAsync();
        }
    }
}
