using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using WebService;
using Domain;
using Microsoft.EntityFrameworkCore;
using WebService.BLL;

namespace tests
{
    public class EntitiesControllerTests
    {


        [Fact]
        public async Task InsertEntity()
        {

            using (var context = new ApplicationDbContext(GetOptions()))
            {
                var controller = new EntitiesController(new EntityService(context));
                // Arrange
                var newEntity = new Entity { Amount = 100.00m };

                // Act
                var result = await controller.InsertEntity(newEntity);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);
                Assert.Equal($"Entity with ID \"{newEntity.Id}\" added.", okResult.Value);
            }
        } 

        [Theory]
        [InlineData(true)]//entity mast bee find
        [InlineData(false)]//entity mast bee not find
        public async Task GetEntity_ReturnsExpectedResult( bool shouldFindEntity)
        { 
            // Arrange
             Guid id= Guid.NewGuid();
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                if (shouldFindEntity)
                {
                    context.Entities.Add(new Entity { Id = id, Amount = 100.00m });
                    await context.SaveChangesAsync();
                }

                var controller = new EntitiesController(new EntityService(context));

                // Act
                var result = await controller.GetEntity(id);

                // Assert
                if (shouldFindEntity)
                {
                    var okResult = Assert.IsType<OkObjectResult>(result);
                    var entity = Assert.IsType<Entity>(okResult.Value);
                    Assert.Equal(id, entity.Id);
                }
                else
                {
                    var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
                    Assert.Contains(id.ToString(), notFoundResult.Value.ToString());
                }
            }
        }

        private static DbContextOptions<ApplicationDbContext> GetOptions()
        {
            var dbName= Guid.NewGuid().ToString();
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }
    }

}


