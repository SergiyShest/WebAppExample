using Microsoft.EntityFrameworkCore;
namespace Domain
{
    public interface IApplicationDbContext
    {
        DbSet<Entity> Entities { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
