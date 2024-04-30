using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public interface IDbContextOptionsFactory
    {
        DbContextOptions<ApplicationDbContext> CreateDbContextOptions();
    }
}
