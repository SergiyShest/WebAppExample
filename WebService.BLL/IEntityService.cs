using Domain;

namespace WebService.BLL
{
    public interface IEntityService<T> where T : class , IEntity
    {
         Task<T> FindAsync(Guid id);

        Task<T> AddAsync(Entity entity);

        Task<T> UpdateAsync(Entity entity);

        Task<T> DeleteAsync(Guid id);
    }
}