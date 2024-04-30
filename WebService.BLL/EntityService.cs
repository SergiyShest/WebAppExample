using Domain;
using Microsoft.EntityFrameworkCore;

namespace WebService.BLL
{
    public class EntityService : IEntityService<Entity>
    {
        private readonly IApplicationDbContext _context;
        public EntityService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Entity> FindAsync(Guid id)
        {
            return await _context.Entities.FindAsync(id);
        }

        public async Task<Entity> AddAsync(Entity entity)
        {
            _context.Entities.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Entity> UpdateAsync(Entity entity)
        {
            var existingEntity = await _context.Entities.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                existingEntity.Amount = entity.Amount;
                existingEntity.OperationDate = entity.OperationDate;
                await _context.SaveChangesAsync();
            }
            return existingEntity;
        }

        public Task<Entity> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }

}

   
