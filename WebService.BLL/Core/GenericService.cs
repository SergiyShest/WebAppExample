using Core;
using DAL.Core;

namespace WebService.BLL.Core
{
    public class GenericService<T> : IGenericService<T> where T : class, IEntity
    {

        IRepositary<T> Repositary
        {
            get
            {
                return _uow.GetRepository<T>();
            }
        }

        protected readonly IUnitOfWork _uow;
        public GenericService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Add(T entity)
        {
            Repositary.Create(entity);
        }

        public async Task<T> FindAsync(Guid id)
        {
            return await Repositary.FindAsync(id);
        }

        public void Delete(T entity)
        {
            Repositary.Delete(entity);
        }

        public void Update(T entity)
        {
            Repositary.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _uow.SaveChangesAsync();
        }
    }

}


