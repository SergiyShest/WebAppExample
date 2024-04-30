using Core;
using Microsoft.EntityFrameworkCore.Storage;

using System.Linq.Expressions;

namespace DAL.Core
{
  public interface IRepositary { }

    public interface IRepositary<TEntity>: IRepositary where TEntity : class ,IEntity
    {
        Task<TEntity> FindAsync(Guid id);

        void Create(TEntity item);

        void Update(TEntity item);

        void Delete(TEntity item);

    }
}
