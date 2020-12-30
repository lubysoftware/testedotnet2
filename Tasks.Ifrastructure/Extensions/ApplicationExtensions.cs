using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tasks.Ifrastructure.Extensions
{
    public static class ApplicationExtensions
    {
        public static void MigrateDatabase<T>(this IApplicationBuilder app) where T : DbContext
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<T>();
                context.Database.Migrate();
            }
        }
    }
}
