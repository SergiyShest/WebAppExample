using Core;
using WebService.DAL.Core;


namespace WebService.BLL.Core
{
    /// <summary>
    /// Generic сервис 
    /// Содержит базовые операции .
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public class GenericService<T> : IGenericService<T> where T : class, IEntity
    {
        protected readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Репозиторий для доступа к сущностям.
        /// </summary>
        private IRepositary<T> Repositary
        {
            get => _uow.GetRepository<T>();
        }

        protected readonly IUnitOfWork _uow;

        /// <summary>
        /// Конструктор сервиса.
        /// </summary>
        public GenericService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Добавляет сущность.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        public void Add(T entity)
        {
            Repositary.Create(entity);
        }

        /// <summary>
        /// Находит сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        public async Task<T> FindAsync(Guid id)
        {
            return await Repositary.FindAsync(id);
        }

        /// <summary>
        /// Удаляет сущность.
        /// </summary>
        public void Delete(T entity)
        {
            Repositary.Delete(entity);
        }

        /// <summary>
        /// Обновляет сущность.
        /// </summary>
        public void Update(T entity)
        {
            Repositary.Update(entity);
        }

        /// <summary>
        /// Сохраняет все изменения.
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _uow.SaveChangesAsync();
        }

        #region IDisposable классическая реализация
        private bool _disposed;

        /// <summary>
        /// Выполняет задачи, связанные с освобождением или сбросом ресурсов.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Освобождает Uow и он в свою очередь DbContext.
        /// </summary>
        /// <param name="disposing">True, если метод вызван явно.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _uow.Dispose();
                }
                _disposed = true;
            }
        }
        #endregion
    }
}





