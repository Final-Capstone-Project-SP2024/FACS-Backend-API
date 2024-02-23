using FireDetection.Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace FireDetection.Backend.API
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using FireDetectionDbContext dbContext = scope.ServiceProvider.GetRequiredService<FireDetectionDbContext>();

            dbContext.Database.Migrate();

        }
    }
}
