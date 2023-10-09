using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyRepos.Presentation.Models;

namespace MyRepos.Presentation.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public virtual DbSet<User> users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}