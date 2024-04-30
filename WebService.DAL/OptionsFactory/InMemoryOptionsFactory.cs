using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebService.DAL.Core;

namespace WebService.DAL.OptionsFactory
{

    public class InMemoryOptionsFactory : IDbContextOptionsFactory
    {
        private readonly string _dbName;

        public InMemoryOptionsFactory(IConfiguration configuration)
        {

            _dbName = configuration["DatabaseConfig:InMemoryName"];

            if (string.IsNullOrEmpty(_dbName))
            {
                throw new ArgumentNullException("The parameter 'DatabaseConfig:InMemoryName' must be defined in the application configuration file.");
            }
        }
        /// <summary>
        /// конструктор для теста
        /// </summary>
        /// <param name="dbName"></param>
        public InMemoryOptionsFactory(string dbName) { 
           _dbName= dbName;
        }

        public DbContextOptions<ApplicationDbContext> CreateDbContextOptions()
        {


            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(_dbName);
            return optionsBuilder.Options;
        }
    }
}
