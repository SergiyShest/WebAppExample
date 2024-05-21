using Microsoft.AspNetCore.Mvc;
using Core;
using WebService.DAL.Core;
using WebService.DAL.OptionsFactory;
using NLog;
using Microsoft.Extensions.Configuration;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class UnitOfWorkTests
    {
        [Theory]
        [InlineData(DbType.Sqlite)]
        [InlineData(DbType.InMemory)]
        public async Task GetEntity_ReturnsExpectedResult(DbType dbType)
        {
            IDbContextOptionsFactory optionsFactory;

            if (dbType == DbType.Sqlite)
            {
                var mockConfiguration = new Mock<IConfiguration>();
                var mockConfigurationSection = new Mock<IConfigurationSection>();

                // Setup the GetSection method to return our mock configuration section
                mockConfiguration.Setup(c => c.GetSection("ConnectionStrings"))
                                 .Returns(mockConfigurationSection.Object);

                // Setup the Value property to return the desired connection string
                mockConfigurationSection.Setup(c => c["SQLiteConnection"])
                                        .Returns("Data Source=:memory:"); // Use in-memory SQLite database for testing

                optionsFactory = new SqliteOptionsFactory(mockConfiguration.Object);
            }
            else
            {
                optionsFactory = new InMemoryOptionsFactory(Guid.NewGuid().ToString());
            }

            // Arrange
            Guid id = Guid.NewGuid();

            using (var uow = new UnitOfWork(optionsFactory))
            {
                await EnsureDatabaseCreatedAsync(uow,dbType);

                uow.GetRepository<Entity>().Create(new Entity { Id = id, Amount = 100.00m });
                await uow.SaveChangesAsync();
            }

            // Act & Assert
            using (var uow = new UnitOfWork(optionsFactory))
            {
                var entity = await uow.GetRepository<Entity>().FindAsync(id);
                Assert.NotNull(entity);
                Assert.Equal(100.00m, entity.Amount);
            }
        }

        private async Task EnsureDatabaseCreatedAsync(UnitOfWork uow, DbType dbType)
        {
            var context = uow.DbContext;
            if (dbType== DbType.Sqlite)
            {
                await context.Database.EnsureCreatedAsync();
                await context.Database.MigrateAsync();
            }
        }
    }

    public enum DbType
    {
        InMemory,
        Sqlite
    }
}


