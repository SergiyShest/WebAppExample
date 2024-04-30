using Core;
using Microsoft.EntityFrameworkCore;
using WebService.DAL.OptionsFactory;
namespace WebService.DAL.Core
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(IDbContextOptionsFactory optionsFactory)
            : base(optionsFactory.CreateDbContextOptions())
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Entity> Entities { get; set; }
    }

}