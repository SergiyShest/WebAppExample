using Core;

namespace WebService.DAL.Core
{
    /// <summary>
    /// Интейрфейс паттерна "Unit of Work".
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Контекст приложения для доступа к базе данных.
        /// </summary>
        IApplicationDbContext DbContext { get; }

        /// <summary>
        /// Возвращает репозиторий для управления сущностями указанного типа.
        /// </summary>
        /// <typeparam name="T">Тип сущности, с которой работает репозиторий.</typeparam>
        /// <returns>Требуемый репозиторий.</returns>
        IRepositary<T> GetRepository<T>() where T : class, IEntity;

        /// <summary>
        /// Асинхронно сохраняет все изменения, сделанные в контексте базы данных.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию сохранения изменений.</returns>
        Task SaveChangesAsync();
    }
}

