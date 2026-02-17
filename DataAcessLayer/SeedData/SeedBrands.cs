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
            "Tucson", "Kona", "Santa Fe", "Bayon", "IONIQ 5" // Hyundai
        };

        // Sedan modelleri - kasa tipi Sedan olacak
        private static readonly HashSet<string> SedanModels = new()
        {
            "3 Serisi", "5 Serisi", "7 Serisi",              // BMW
            "C Serisi", "E Serisi", "S Serisi", "CLA", "EQE", "EQS", // Mercedes
            "A4", "A6", "A8",                                // Audi
            "Passat", "Jetta", "Arteon",                     // VW
            "Corolla", "Camry",                              // Toyota
            "S60", "S90",                                    // Volvo
            "Model 3", "Model S",                            // Tesla
            "Civic", "City", "Accord",                       // Honda
            "Focus", "Mondeo",                               // Ford
            "Elantra", "Sonata"                              // Hyundai
        };

        // Hatchback modelleri - kasa tipi Hatchback olacak
        private static readonly HashSet<string> HatchbackModels = new()
        {
            "1 Serisi",                                      // BMW
            "A Serisi",                                      // Mercedes
            "A1", "A3",                                      // Audi
            "Golf", "Polo",                                  // VW
            "Yaris",                                         // Toyota
            "Jazz",                                          // Honda
            "Fiesta",                                        // Ford
            "i10", "i20", "i30"                              // Hyundai
        };

        // Pickup modelleri - kasa tipi Pickup olacak
        private static readonly HashSet<string> PickupModels = new()
        {
            "Amarok",                                        // VW
            "Hilux",                                         // Toyota
            "Ranger"                                         // Ford
        };

        public static async Task SeedAsync(Context context)
        {
            if (await context.Set<Brand>().AnyAsync())
                return;

            // BodyType'ları al
            var bodyTypes = await context.Set<BodyType>().ToListAsync();
            var suvBodyType = bodyTypes.FirstOrDefault(b => b.Name == "SUV");
            var sedanBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Sedan");
            var hatchbackBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Hatchback");
            var pickupBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Pickup");

            var brandModels = new Dictionary<string, string[]>
            {
                { "BMW", new[] { "1 Serisi", "3 Serisi", "5 Serisi", "X1", "X3", "X5", "iX" } },
                { "Mercedes", new[] { "A Serisi", "C Serisi", "E Serisi", "S Serisi", "CLA", "G Serisi", "EQE", "EQS" } },
                { "Audi", new[] { "A1", "A3", "A4", "A6", "Q3", "Q5", "Q7", "Q8", "e-tron" } },
                { "Volkswagen", new[] { "Golf", "Polo", "Passat", "Tiguan", "Touareg", "Amarok" } },
                { "Toyota", new[] { "Corolla", "Yaris", "C-HR", "RAV4", "Hilux", "Land Cruiser" } },
                { "Volvo", new[] { "S60", "S90", "XC40", "XC60", "XC90" } },
                { "Tesla", new[] { "Model 3", "Model Y", "Model S", "Model X" } },
                { "Honda", new[] { "Civic", "City", "HR-V", "CR-V", "Jazz", "Accord" } },
                { "Ford", new[] { "Focus", "Fiesta", "Puma", "Kuga", "Ranger" } },
                { "Hyundai", new[] { "i10", "i20", "i30", "Elantra", "Tucson", "Bayon", "IONIQ 5" } }
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
                    Guid? bodyTypeId = null;
                    if (SuvModels.Contains(modelName))
                        bodyTypeId = suvBodyType?.Id;
                    else if (SedanModels.Contains(modelName))
                        bodyTypeId = sedanBodyType?.Id;
                    else if (HatchbackModels.Contains(modelName))
                        bodyTypeId = hatchbackBodyType?.Id;
                    else if (PickupModels.Contains(modelName))
                        bodyTypeId = pickupBodyType?.Id;

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
        /// Mevcut markalara eksik modelleri ekler (yeni model seed sonrası için)
        /// </summary>
        public static async Task AddMissingModelsAsync(Context context)
        {
            var bodyTypes = await context.Set<BodyType>().ToListAsync();
            var suvBodyType = bodyTypes.FirstOrDefault(b => b.Name == "SUV");
            var sedanBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Sedan");
            var hatchbackBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Hatchback");
            var pickupBodyType = bodyTypes.FirstOrDefault(b => b.Name == "Pickup");

            var brandModels = new Dictionary<string, string[]>
            {
                { "BMW", new[] { "1 Serisi" } },
                { "Mercedes", new[] { "A Serisi" } },
                { "Audi", new[] { "A1" } },
                { "Honda", new[] { "Jazz", "Accord" } },
                { "Hyundai", new[] { "i30" } }
            };

            var existingBrands = await context.Set<Brand>()
                .Include(b => b.Models)
                .ToListAsync();

            var added = false;

            foreach (var entry in brandModels)
            {
                var brand = existingBrands.FirstOrDefault(b => b.Name == entry.Key);
                if (brand == null) continue;

                var existingModelNames = brand.Models.Select(m => m.Name).ToHashSet();

                foreach (var modelName in entry.Value)
                {
                    if (existingModelNames.Contains(modelName)) continue;

                    Guid? bodyTypeId = null;
                    if (SuvModels.Contains(modelName))
                        bodyTypeId = suvBodyType?.Id;
                    else if (SedanModels.Contains(modelName))
                        bodyTypeId = sedanBodyType?.Id;
                    else if (HatchbackModels.Contains(modelName))
                        bodyTypeId = hatchbackBodyType?.Id;
                    else if (PickupModels.Contains(modelName))
                        bodyTypeId = pickupBodyType?.Id;

                    context.Set<Model>().Add(new Model
                    {
                        Id = Guid.NewGuid(),
                        BrandId = brand.Id,
                        BodyTypeId = bodyTypeId,
                        Name = modelName,
                        CreatedAt = DateTime.UtcNow
                    });

                    added = true;
                }
            }

            if (added)
                await context.SaveChangesAsync();
        }

        /// <summary>
        /// Mevcut modellere BodyTypeId ataması yapar - RAW SQL ile (EF Core change tracking bypass)
        /// ÖNEMLI: BodyType'ları doğru VehicleType altından seçer (duplicate body type sorunu için)
        /// </summary>
        public static async Task UpdateModelBodyTypesAsync(Context context)
        {
            context.ChangeTracker.Clear();

            // VehicleType'ları al - doğru parent'ı bulmak için
            var vehicleTypes = await context.Set<VehicleType>().AsNoTracking().ToListAsync();
            var otomobilVT = vehicleTypes.FirstOrDefault(vt => vt.Name == "Otomobil");
            var suvAraziVT = vehicleTypes.FirstOrDefault(vt => vt.Name == "SUV & Arazi Araçları");

            Console.WriteLine($"[Seed] VehicleTypes -> Otomobil: {otomobilVT?.Id}, SUV & Arazi: {suvAraziVT?.Id}");

            var bodyTypes = await context.Set<BodyType>().AsNoTracking().ToListAsync();

            // Doğru VehicleType altındaki BodyType'ları seç
            // Sedan ve Hatchback -> "Otomobil" altında olmalı
            // SUV ve Pickup -> "SUV & Arazi Araçları" altında olmalı
            var sedanBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Sedan" && bt.VehicleTypeId == otomobilVT?.Id);
            var hatchbackBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Hatchback" && bt.VehicleTypeId == otomobilVT?.Id);
            var suvBT = bodyTypes.FirstOrDefault(bt => bt.Name == "SUV" && bt.VehicleTypeId == suvAraziVT?.Id);
            var pickupBT = bodyTypes.FirstOrDefault(bt => bt.Name == "Pickup" && bt.VehicleTypeId == suvAraziVT?.Id);

            Console.WriteLine($"[Seed] Correct BodyTypes -> Sedan: {sedanBT?.Id}, Hatchback: {hatchbackBT?.Id}, SUV: {suvBT?.Id}, Pickup: {pickupBT?.Id}");

            var updates = new Dictionary<string, (BodyType? bodyType, HashSet<string> models)>
            {
                { "Sedan", (sedanBT, SedanModels) },
                { "Hatchback", (hatchbackBT, HatchbackModels) },
                { "SUV", (suvBT, SuvModels) },
                { "Pickup", (pickupBT, PickupModels) }
            };

            var totalUpdated = 0;
            foreach (var entry in updates)
            {
                if (entry.Value.bodyType == null)
                {
                    Console.WriteLine($"[Seed] WARNING: BodyType '{entry.Key}' not found under correct VehicleType!");
                    continue;
                }

                var bodyTypeId = entry.Value.bodyType.Id;
                var inClause = string.Join(", ", entry.Value.models.Select(n => $"'{n.Replace("'", "''")}'"));
                var sql = $@"UPDATE ""Models"" SET ""BodyTypeId"" = '{bodyTypeId}' WHERE ""Name"" IN ({inClause}) AND (""BodyTypeId"" IS NULL OR ""BodyTypeId"" != '{bodyTypeId}')";

                var affected = await context.Database.ExecuteSqlRawAsync(sql);
                Console.WriteLine($"[Seed] {entry.Key} (Id={bodyTypeId}): {affected} models updated via raw SQL");
                totalUpdated += affected;
            }

            Console.WriteLine($"[Seed] === Total models updated: {totalUpdated} ===");

            // Yanlış VehicleType'a ait orphan BodyType'ları temizle (duplicate olanlar)
            await CleanupOrphanBodyTypesAsync(context, otomobilVT, suvAraziVT);
        }

        /// <summary>
        /// Yanlış VehicleType altındaki duplicate BodyType kayıtlarını temizlemeye çalışır (hata olursa atlar)
        /// </summary>
        private static async Task CleanupOrphanBodyTypesAsync(Context context, VehicleType? otomobilVT, VehicleType? suvAraziVT)
        {
            if (otomobilVT == null || suvAraziVT == null) return;

            try
            {
                var validVehicleTypeIds = new[] { otomobilVT.Id, suvAraziVT.Id };
                var inClause = string.Join(", ", validVehicleTypeIds.Select(id => $"'{id}'"));

                // Hiçbir tabloda referans edilmeyen orphan BodyType'ları sil
                var sql = $@"
                    DELETE FROM ""BodyTypes""
                    WHERE ""VehicleTypeId"" NOT IN ({inClause})
                    AND ""Id"" NOT IN (SELECT DISTINCT ""BodyTypeId"" FROM ""Models"" WHERE ""BodyTypeId"" IS NOT NULL)
                    AND ""Id"" NOT IN (SELECT DISTINCT ""BodyTypeId"" FROM ""Vehicles"" WHERE ""BodyTypeId"" IS NOT NULL)";

                var deleted = await context.Database.ExecuteSqlRawAsync(sql);
                if (deleted > 0)
                    Console.WriteLine($"[Seed] {deleted} orphan BodyType records cleaned up.");

                // Hiçbir tabloda referans edilmeyen orphan VehicleType'ları sil
                var cleanupVT = $@"
                    DELETE FROM ""VehicleTypes""
                    WHERE ""Id"" NOT IN ({inClause})
                    AND ""Id"" NOT IN (SELECT DISTINCT ""VehicleTypeId"" FROM ""BodyTypes"")
                    AND ""Id"" NOT IN (SELECT DISTINCT ""VehicleTypeId"" FROM ""Vehicles"" WHERE ""VehicleTypeId"" IS NOT NULL)";

                var deletedVT = await context.Database.ExecuteSqlRawAsync(cleanupVT);
                if (deletedVT > 0)
                    Console.WriteLine($"[Seed] {deletedVT} orphan VehicleType records cleaned up.");
            }
            catch (Exception ex)
            {
                // FK constraint varsa silme işlemini atla - kritik değil
                Console.WriteLine($"[Seed] Orphan cleanup skipped (FK constraint): {ex.Message}");
            }
        }
    }
}
