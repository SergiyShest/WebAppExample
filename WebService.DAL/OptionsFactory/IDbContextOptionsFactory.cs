using Microsoft.EntityFrameworkCore;
using WebService.DAL.Core;

namespace WebService.DAL.OptionsFactory
{
    public interface IDbContextOptionsFactory
    {
        DbContextOptions<ApplicationDbContext> CreateDbContextOptions();
    }
}
