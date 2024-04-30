using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Domain
{

    public class InMemoryOptionsFactory : IDbContextOptionsFactory
    {
        private readonly string _dbName="hhhhhhhhh";

        public InMemoryOptionsFactory(IConfiguration configuration)
        {
            
            _dbName = configuration["DatabaseConfig:InMemoryName"];

            if (string.IsNullOrEmpty(_dbName))
            {
                throw new ArgumentNullException("The parameter 'DatabaseConfig:InMemoryName' must be defined in the application configuration file.");
            }
        }
        public DbContextOptions<ApplicationDbContext> CreateDbContextOptions()
        {
          

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(_dbName);
            return optionsBuilder.Options;
        }
    }
}
