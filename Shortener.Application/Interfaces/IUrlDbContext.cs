using Microsoft.EntityFrameworkCore;
using Shortener.Domain;

namespace Shortener.Application.Interfaces
{
    public interface IUrlDbContext
    {
        DbSet<Url> Urls { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Redirection> Redirections { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}