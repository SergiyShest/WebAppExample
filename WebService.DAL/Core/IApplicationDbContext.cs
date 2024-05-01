using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebService.DAL.Core
{
    /// <summary>
    /// Интерфейс контекста приложения для взаимодействия с базой данных.
    /// Определяет методы и свойства, необходимые для работы с DbContext.
    /// Методы могут быть расширены при необходимости.
    /// </summary>
    public interface IApplicationDbContext : IDisposable
    {
        /// <summary>
        /// Предоставляет доступ к функциям базы данных, таким как транзакции и запросы к базе данных.
        /// </summary>
        DatabaseFacade Database { get; }

        /// <summary>
        /// Асинхронно сохраняет все изменения, сделанные в контексте базы данных.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены для отмены операции.</param>
        /// <returns>Количество измененных записей в базе данных.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Предоставляет информацию о состоянии и управление состоянием указанной сущности.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entity">Сущность, для которой необходимо получить информацию о состоянии.</param>
        /// <returns>Объект EntityEntry, предоставляющий доступ к информации о состоянии и управлению состоянием сущности.</returns>
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Возвращает DbSet для сущностей указанного типа, что позволяет проводить операции с коллекцией данных.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <returns>DbSet сущностей указанного типа.</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
