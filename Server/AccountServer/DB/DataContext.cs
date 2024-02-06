using AccountServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountServer.DB
{
    public class DataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Oauth> Oauths { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Account>()
                .HasIndex(a => a.AccountId)
                .IsUnique();

            builder.Entity<Oauth>()
                .HasIndex(o => o.OauthId)
                .IsUnique();
        }
    }
}
