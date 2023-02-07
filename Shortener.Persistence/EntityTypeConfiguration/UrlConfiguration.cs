using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shortener.Domain;

namespace Shortener.Persistence.EntityTypeConfiguration
{
    internal class UrlConfiguration : IEntityTypeConfiguration<Url>
    {
        public void Configure(EntityTypeBuilder<Url> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.Property(x => x.Title).HasMaxLength(250);
            builder.Property(x => x.Description).HasMaxLength(250);
        }
    }
}
