using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using WebService;
using Core;
using Microsoft.EntityFrameworkCore;
using WebService.BLL;
using WebService.DAL.Core;
using DAL.Core;
using WebService.DAL.OptionsFactory;

namespace tests
{
    public class EntitiesControllerTests
    {


        [Fact]
        public async Task InsertEntity()
        {
            using (var uow = new UnitOfWork(new InMemoryOptionsFactory(Guid.NewGuid().ToString())))
            {
                var controller = new EntitiesController(new EntityService(uow));
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
        public async Task GetEntity_ReturnsExpectedResult(bool shouldFindEntity)
        {
            // Arrange
            Guid id = Guid.NewGuid();
            using (var uow = new UnitOfWork(new InMemoryOptionsFactory(Guid.NewGuid().ToString())))
            {
                if (shouldFindEntity)
                {
                    uow.GetRepository<Entity>().Create(new Entity { Id = id, Amount = 100.00m });
                    await uow.SaveChangesAsync();
                }

                var controller = new EntitiesController(new EntityService(uow));

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
    }
}


