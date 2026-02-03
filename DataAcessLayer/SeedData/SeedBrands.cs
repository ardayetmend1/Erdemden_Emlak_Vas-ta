using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.SeedData
{
    public static class SeedBrands
    {
        public static async Task SeedAsync(Context context)
        {
            if (await context.Set<Brand>().AnyAsync())
                return;

            var brandModels = new Dictionary<string, string[]>
            {
                { "BMW", new[] { "3 Serisi", "5 Serisi", "X1", "X3", "X5", "iX" } },
                { "Mercedes", new[] { "C Serisi", "E Serisi", "S Serisi", "CLA", "G Serisi", "EQE", "EQS" } },
                { "Audi", new[] { "A3", "A4", "A6", "Q3", "Q5", "Q7", "Q8", "e-tron" } },
                { "Volkswagen", new[] { "Golf", "Polo", "Passat", "Tiguan", "Touareg", "Amarok" } },
                { "Toyota", new[] { "Corolla", "Yaris", "C-HR", "RAV4", "Hilux", "Land Cruiser" } },
                { "Volvo", new[] { "S60", "S90", "XC40", "XC60", "XC90" } },
                { "Tesla", new[] { "Model 3", "Model Y", "Model S", "Model X" } },
                { "Honda", new[] { "Civic", "City", "HR-V", "CR-V" } },
                { "Ford", new[] { "Focus", "Fiesta", "Puma", "Kuga", "Ranger" } },
                { "Hyundai", new[] { "i10", "i20", "Elantra", "Tucson", "Bayon", "IONIQ 5" } }
            };

            foreach (var brandData in brandModels)
            {
                var brand = new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = brandData.Key,
                    CreatedAt = DateTime.UtcNow
                };

                context.Set<Brand>().Add(brand);

                foreach (var modelName in brandData.Value)
                {
                    var model = new Model
                    {
                        Id = Guid.NewGuid(),
                        BrandId = brand.Id,
                        Name = modelName,
                        CreatedAt = DateTime.UtcNow
                    };

                    context.Set<Model>().Add(model);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
