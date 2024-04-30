
using Core;
using WebService.DAL.Core;
using WebService.DAL.OptionsFactory;

namespace DAL.Core
{

    public class UnitOfWork : IUnitOfWork
    {

        private readonly Dictionary<string, IRepositary> _repos = new Dictionary<string, IRepositary>();

        public IApplicationDbContext DbContext { get; }

        public UnitOfWork(IDbContextOptionsFactory optionsFactory)
        {
            DbContext = new ApplicationDbContext(optionsFactory);
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }
       
        public IRepositary<T> GetRepository<T>() where T : class, IEntity
        {
            IRepositary<T> rep;
            if (_repos.ContainsKey(typeof(T).Name))
            {
                rep = (IRepositary<T>)_repos[typeof(T).Name];
            }
            else
            {
                rep = new Repository<T>(this.DbContext);
                _repos.Add(typeof(T).Name, rep);
            }

            return rep;
        }


        #region IDicposable realisation
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
                _disposed = true;
            }
        }


        #endregion
    }

}