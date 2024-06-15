namespace PartLyAPi.Models
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<UrlMapper> UrlMapper { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :
            base(options)
        { }
    }
}
