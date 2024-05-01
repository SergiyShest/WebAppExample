using Core;
using WebService.DAL.OptionsFactory;


    namespace WebService.DAL.Core
    {
        /// <summary>
        /// Реализует паттерн "UnitOfWork".
        /// </summary>
        public class UnitOfWork : IUnitOfWork
           {
            private readonly Dictionary<string, IRepositary> _repos = new Dictionary<string, IRepositary>();

            /// <summary>
            /// Контекст базы данных.
            /// </summary>
            public IApplicationDbContext DbContext { get; }

            /// <summary>  
            /// Инициализирует новый экземпляр UnitOfWork.
            /// </summary>
            /// <param name="optionsFactory">Фабрика для создания настроек контекста базы данных (изменение параметра позволяет изменять базу данных).</param>
            public UnitOfWork(IDbContextOptionsFactory optionsFactory)
            {
                DbContext = new ApplicationDbContext(optionsFactory);
            }

            /// <summary>        
            /// Асинхронно сохраняет все изменения в контексте базы данных.
            /// </summary>
            /// <returns>Задача, представляющая асинхронную операцию.</returns>
            public async Task SaveChangesAsync()
            {
                await DbContext.SaveChangesAsync();
            }

            /// <summary>
            /// Получает репозиторий для указанного типа сущности, создавая его при необходимости.
            /// </summary>
            /// <typeparam name="T">Тип сущности.</typeparam>
            /// <returns>Репозиторий для указанного типа сущности.</returns>
            public IRepositary<T> GetRepository<T>() where T : class, IEntity
            {
                if (!_repos.ContainsKey(typeof(T).Name))
                {
                    _repos[typeof(T).Name] = new Repository<T>(this.DbContext);
                }
                return (IRepositary<T>)_repos[typeof(T).Name];
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
            /// Освобождает DbContext.
            /// </summary>
            /// <param name="disposing">True, если метод вызван явно.</param>
            protected virtual void Dispose(bool disposing)
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
