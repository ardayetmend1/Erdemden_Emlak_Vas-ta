using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.SeedData
{
    public static class SeedCities
    {
        public static async Task SeedAsync(Context context)
        {
            if (await context.Set<City>().AnyAsync())
                return;

            var citiesData = new Dictionary<string, string[]>
            {
                { "İstanbul", new[] { "Kadıköy", "Beşiktaş", "Üsküdar", "Şişli", "Bakırköy", "Sarıyer" } },
                { "Ankara", new[] { "Çankaya", "Keçiören", "Mamak", "Yenimahalle" } },
                { "İzmir", new[] { "Konak", "Bornova", "Karşıyaka", "Alsancak", "Çeşme" } },
                { "Bursa", new[] { "Nilüfer", "Osmangazi", "Yıldırım" } },
                { "Antalya", new[] { "Konyaaltı", "Muratpaşa", "Kepez", "Alanya" } },
                { "Muğla", new[] { "Bodrum", "Marmaris", "Fethiye", "Datça" } },
                { "Bolu", new[] { "Merkez", "Abant", "Gerede" } }
            };

            foreach (var cityData in citiesData)
            {
                var city = new City
                {
                    Id = Guid.NewGuid(),
                    Name = cityData.Key,
                    CreatedAt = DateTime.UtcNow
                };

                context.Set<City>().Add(city);

                foreach (var districtName in cityData.Value)
                {
                    var district = new District
                    {
                        Id = Guid.NewGuid(),
                        CityId = city.Id,
                        Name = districtName,
                        CreatedAt = DateTime.UtcNow
                    };

                    context.Set<District>().Add(district);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
