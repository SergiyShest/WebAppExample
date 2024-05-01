using Core;

namespace WebService.DAL.Core
{
    /// <summary>
    /// Базовый интерфейс репозитория, используется для идентификации типа репозитория независимо от реализации IEntity.
    /// </summary>
    public interface IRepositary { }

    /// <summary>
    /// Интерфейс репозитория для работы с сущностями типа IEntity.
    /// Обеспечивает методы для поиска, создания, обновления и удаления сущностей.
    /// Вызов методов Create, Update, Delete не приводит к немедленному сохранению изменений.
    /// Для сохранения изменений следует использовать метод SaveChangesAsync из IApplicationDbContext.
    /// Интерфейс может быть расширен при необходимости.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности, с которой работает репозиторий.</typeparam>
    public interface IRepositary<TEntity> : IRepositary where TEntity : class, IEntity
    {
        /// <summary>
        /// Асинхронно находит сущность по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Сущность, если она найдена; иначе null.</returns>
        Task<TEntity> FindAsync(Guid id);

        /// <summary>
        /// Создает новую сущность в базе данных.
        /// </summary>
        /// <param name="item">Сущность для создания.</param>
        void Create(TEntity item);

        /// <summary>
        /// Обновляет существующую сущность в базе данных.
        /// </summary>
        /// <param name="item">Сущность для обновления.</param>
        void Update(TEntity item);

        /// <summary>
        /// Удаляет сущность из базы данных.
        /// </summary>
        /// <param name="item">Сущность для удаления.</param>
        void Delete(TEntity item);
    }
}
