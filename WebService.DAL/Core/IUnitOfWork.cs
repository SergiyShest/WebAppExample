using Core;
using WebService.DAL.Core;

namespace DAL.Core
{
    public interface IUnitOfWork :IDisposable
    {

        IApplicationDbContext DbContext { get; }

        IRepositary<T> GetRepository<T>() where T : class, IEntity;

        Task SaveChangesAsync();

    }



}
