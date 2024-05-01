using Core;
using Microsoft.Extensions.Logging;
using WebService.BLL.Core;
using WebService.DAL.Core;

namespace WebService.BLL
{
    /// <summary>
    /// Сервис для  Entity.
    /// </summary>
    public class EntityService : GenericService<Entity>, IEntityService
    {


        public EntityService(IUnitOfWork uow) : base(uow) { }


        /// <summary>
        /// Добавление Entity с сохранением в базу 
        /// Если Entity с таким идентификатором существует, то бросаю исключение
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Entity</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Entity> AddEntityAsync(Entity entity)
        {
            try
            {
                var existingEntity = await FindAsync(entity.Id);
                if (existingEntity != null)
                {
                    throw new InvalidOperationException($"Entity with ID {entity.Id} already exists.");
                }

                Add(entity);
                await SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding entity with ID {EntityId}", entity.Id);
                throw; //пробросить исключение дальше
            }
        }
        
        /// <summary>
        /// Поиск существующей Entity
        /// Если не найдена будет брошено исключение
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<Entity> GetEntityAsync(Guid id)
        {
            try
            {
                var entity = await FindAsync(id);
                if (entity == null)
                {
                    throw new KeyNotFoundException($"Entity with ID {id} not found.");
                }
                return entity;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error retrieving entity with ID {EntityId}", id);
                throw; // пробросить исключение дальше
            }
        }
    }
}


