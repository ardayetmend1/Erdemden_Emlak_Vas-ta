using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.SeedData
{
    public static class SeedTransmissionTypes
    {
        public static async Task SeedAsync(Context context)
        {
            if (await context.Set<TransmissionType>().AnyAsync())
                return;

            var transmissionTypes = new[] { "Otomatik", "Manuel", "Yarı Otomatik" };

            foreach (var transmissionTypeName in transmissionTypes)
            {
                var transmissionType = new TransmissionType
                {
                    Id = Guid.NewGuid(),
                    Name = transmissionTypeName,
                    CreatedAt = DateTime.UtcNow
                };

                context.Set<TransmissionType>().Add(transmissionType);
            }

            await context.SaveChangesAsync();
        }
    }
}
