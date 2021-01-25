
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Desafio.WebApp.Areas.Identity.Data
{
    public class DesafioIdentityContext : IdentityDbContext
    {
        public DesafioIdentityContext(DbContextOptions<DesafioIdentityContext> options)
            : base(options)
        {
        }

    }
}
