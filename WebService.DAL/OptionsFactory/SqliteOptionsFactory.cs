using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebService.DAL.Core;

namespace WebService.DAL.OptionsFactory
{
    public class SqliteOptionsFactory : IDbContextOptionsFactory
    {
        private readonly string _connectionString;

        public SqliteOptionsFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SQLiteConnection");
        }

        public DbContextOptions<ApplicationDbContext> CreateDbContextOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite(_connectionString);
            return optionsBuilder.Options;
        }
    }
}
