using Microsoft.AspNetCore.Mvc;
using Core;
using WebService.DAL.Core;
using WebService.DAL.OptionsFactory;
using NLog;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests
{
    public class UowTests
    {

        [Theory]
        [InlineData(DbType.InMemory)]//entity mast bee find
        [InlineData(DbType.Sqlite)]//entity mast bee not find
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
                                        .Returns("YourConnectionStringHere");

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

                uow.GetRepository<Entity>().Create(new Entity { Id = id, Amount = 100.00m });
                await uow.SaveChangesAsync();
            }




        }

        public enum DbType
        {
            InMemory,
            Sqlite
        }

    }

}


