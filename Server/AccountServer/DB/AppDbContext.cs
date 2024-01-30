using AccountServer.Model;
using Microsoft.EntityFrameworkCore;

namespace AccountServer.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<PlayerDb> Player { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PlayerDb>()
                .HasIndex(p => p.PlayerId)
                .IsUnique();
        }
    }
}
