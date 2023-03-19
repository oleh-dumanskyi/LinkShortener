using Microsoft.EntityFrameworkCore;
using Shortener.Application.Interfaces;
using Shortener.Domain;
using Shortener.Persistence.EntityTypeConfiguration;

namespace Shortener.Persistence
{
    public sealed class UrlDbContext : DbContext, IUrlDbContext
    {
        public DbSet<Url> Urls { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Redirection> Redirections { get; set; }
        public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UrlConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RedirectionConfiguration());
            base.OnModelCreating(builder);
        }
    }
}