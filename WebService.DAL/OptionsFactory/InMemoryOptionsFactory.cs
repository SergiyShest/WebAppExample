using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebService.DAL.Core;

namespace WebService.DAL.OptionsFactory
{

    // <summary>
    /// Фабрика для создания опций DbContext с типом InMemoryDatabase.
    /// </summary>
    public class InMemoryOptionsFactory : IDbContextOptionsFactory
    {
        private readonly string _dbName;

        /// <summary>
        /// Основной конструктор
        /// </summary>
        public InMemoryOptionsFactory(IConfiguration configuration)
        {
            _dbName = configuration["DatabaseConfig:InMemoryName"];
            if (string.IsNullOrEmpty(_dbName))
            {
                throw new ArgumentNullException("The parameter 'DatabaseConfig:InMemoryName' must be defined in the application configuration file.");
            }
        }

        /// <summary>
        /// Конструктор для создания экземпляра с заданным именем базы данных.
        /// Используется для тестирования.
        /// </summary>
        /// <param name="dbName">Имя базы данных.</param>
        public InMemoryOptionsFactory(string dbName)
        {
            _dbName = dbName;
        }

        /// <summary>
        /// DbContextOptions для InMemoryDatabase.
        /// </summary>
        public DbContextOptions<ApplicationDbContext> CreateDbContextOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(_dbName);
            return optionsBuilder.Options;
        }
    }
}
