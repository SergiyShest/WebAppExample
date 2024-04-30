using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Domain
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
