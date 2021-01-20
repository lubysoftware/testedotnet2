using luby_app.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace luby_app.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            } 

            var devRole = new IdentityRole("Desenvolvedor");

            if (roleManager.Roles.All(r => r.Name != devRole.Name))
            {
                await roleManager.CreateAsync(devRole);
            }

            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost", EmailConfirmed = true };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                var result = await userManager.CreateAsync(administrator, "123456");

                await userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary 
        }
    }
}
