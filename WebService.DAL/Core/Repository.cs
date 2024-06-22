using Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace WebService.DAL.Core
{
    /// <summary>
    /// Репозиторий для работы с сущностями типа IEntity.
    /// Обеспечивает методы для поиска, создания, обновления и удаления сущностей.
    /// Вызов методов Create, Update, Delete не приводит к немедленному сохранению изменений.
    /// Для сохранения изменений следует использовать метод SaveChangesAsync из IApplicationDbContext.
    /// Может быть расширен при необходимости.
    /// </summary>
    public class Repository<T> : IRepositary<T> where T : class, IEntity
    {
        private readonly IApplicationDbContext _db;
        
        #region Конструктор

        /// <summary>
        /// Инициализирует новый экземпляр репозитория с указанным контекстом приложения.
        /// </summary>
        /// <param name="db">Контекст приложения для работы с базой данных.</param>
        public Repository(IApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        #endregion


        #region Получение сущьностей

        /// <summary>
        /// Асинхронно возвращает IQueryable для всей таблицы .
        /// </summary>
        public async Task<IQueryable<T>> GetAllAsync()
        {
            return await Task.FromResult(_db.Set<T>().AsQueryable());
        }


        /// <summary>
        /// Асинхронно находит сущность по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Асинхронная задача, результатом которой является найденная сущность.</returns>
        public async Task<T> FindAsync(Guid id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        #endregion

        #region Изменение сущьностей


        /// <summary>
        /// Создает новую сущность в базе данных.
        /// </summary>
        /// <param name="item">Сущность для добавления.</param>
        public void Create(T item)
        {
            _db.Set<T>().Add(item);
        }

        /// <summary>
        /// Обновляет существующую сущность в базе данных.
        /// </summary>
        /// <param name="item">Сущность для обновления.</param>
        public void Update(T item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        /// Удаляет сущность из базы данных.
        /// </summary>
        /// <param name="item">Сущность для удаления.</param>
        public void Delete(T item)
        {
            _db.Entry(item).State = EntityState.Deleted;
        } 
        #endregion


    }
}