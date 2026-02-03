using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.SeedData
{
    public static class SeedBrands
    {
        // SUV modelleri - kasa tipi SUV olacak
        private static readonly HashSet<string> SuvModels = new()
        {
            "X1", "X3", "X5", "X7", "iX",                    // BMW
            "GLA", "GLB", "GLC", "GLE", "GLS", "G Serisi",   // Mercedes
            "Q3", "Q5", "Q7", "Q8", "e-tron",                // Audi
            "Tiguan", "Touareg", "T-Roc",                    // VW
            "C-HR", "RAV4", "Land Cruiser",                  // Toyota
            "XC40", "XC60", "XC90",                          // Volvo
            "Model Y", "Model X",                            // Tesla
            "HR-V", "CR-V",                                  // Honda
            "Puma", "Kuga", "Explorer",                      // Ford
            "Tucson", "Kona", "Santa Fe", "Bayon"            // Hyundai
        };

        // Sedan modelleri - kasa tipi Sedan olacak
        private static readonly HashSet<string> SedanModels = new()
        {
            "3 Serisi", "5 Serisi", "7 Serisi",              // BMW
            "C Serisi", "E Serisi", "S Serisi", "CLA",       // Mercedes
            "A3", "A4", "A6", "A8",                          // Audi
            "Passat", "Jetta", "Arteon",                     // VW
            "Corolla", "Camry",                              // Toyota
            "S60", "S90",                                    // Volvo
            "Model 3", "Model S",                            // Tesla
            "Civic", "City", "Accord",                       // Honda
            "Focus", "Mondeo",                               // Ford
            "Elantra", "Sonata", "i10", "i20"                // Hyundai
        };

        public static async Task SeedAsync(Context context)
        {
            if (await context.Set<Brand>().AnyAsync())
                return;

            // Önce BodyType'ları al (sadece SUV ve Sedan)
            var bodyTypes = await context.Set<BodyType>().ToListAsync();
            var suvBodyType = bodyTypes.FirstOrDefault(b => b.Name == "SUV");
            var sedanBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Sedan");

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
                    // Model ismine göre BodyTypeId belirle (sadece SUV ve Sedan)
                    Guid? bodyTypeId = null;
                    if (SuvModels.Contains(modelName))
                        bodyTypeId = suvBodyType?.Id;
                    else if (SedanModels.Contains(modelName))
                        bodyTypeId = sedanBodyType?.Id;
                    // Diğer modeller (Hatchback, Pickup vs.) null kalır - tüm listelerde görünür

                    var model = new Model
                    {
                        Id = Guid.NewGuid(),
                        BrandId = brand.Id,
                        BodyTypeId = bodyTypeId,
                        Name = modelName,
                        CreatedAt = DateTime.UtcNow
                    };

                    context.Set<Model>().Add(model);
                }
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Mevcut modellere BodyTypeId ataması yapar (migration sonrası için)
        /// </summary>
        public static async Task UpdateModelBodyTypesAsync(Context context)
        {
            var bodyTypes = await context.Set<BodyType>().ToListAsync();
            var suvBodyType = bodyTypes.FirstOrDefault(b => b.Name == "SUV");
            var sedanBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Sedan");

            var models = await context.Set<Model>().ToListAsync();

            foreach (var model in models)
            {
                if (model.BodyTypeId != null) continue; // Zaten atanmış

                if (SuvModels.Contains(model.Name))
                    model.BodyTypeId = suvBodyType?.Id;
                else if (SedanModels.Contains(model.Name))
                    model.BodyTypeId = sedanBodyType?.Id;
                // Diğer modeller null kalır - tüm listelerde görünür
            }

            await context.SaveChangesAsync();
        }
    }
}
