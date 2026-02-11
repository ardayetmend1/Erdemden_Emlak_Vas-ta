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
    }
}
