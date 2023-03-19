using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shortener.Domain;

namespace Shortener.Persistence.EntityTypeConfiguration
{
    public class RedirectionConfiguration : IEntityTypeConfiguration<Redirection>
    {
        public void Configure(EntityTypeBuilder<Redirection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.ToTable(name: "Redirections");
        }
    }
}
