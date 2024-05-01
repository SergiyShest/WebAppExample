using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebService.DAL.Core;

namespace WebService.DAL.OptionsFactory
{
    /// <summary>
    /// Фабрика создания опций DbContext для SQLite базы данных.
    /// </summary>
    public class SqliteOptionsFactory : IDbContextOptionsFactory
    {
        private readonly string _connectionString;

        /// <summary>
        /// Конструктор
        /// </summary>
        public SqliteOptionsFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SQLiteConnection");
        }

        /// <summary>
        /// DbContextOptions для SQLite базы данных.
        /// </summary>
        public DbContextOptions<ApplicationDbContext> CreateDbContextOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite(_connectionString);
            return optionsBuilder.Options;
        }
    }
}
