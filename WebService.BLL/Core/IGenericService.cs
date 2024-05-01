using Core;

namespace WebService.BLL.Core
{
    public interface IGenericService<T>:IDisposable where T : class, IEntity
    {
        Task<T> FindAsync(Guid id);

        void Add(T entity);

        Task SaveChangesAsync();

        void Update(T entity);

        void Delete(T entity);
    }
}