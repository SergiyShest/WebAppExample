using Core;
using Microsoft.EntityFrameworkCore;
using WebService.DAL.OptionsFactory;
namespace WebService.DAL.Core
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        /// <summary>
        /// Конструктор, использующий фабрику для создания настроек DbContext.
        /// Позволяет менять базу данных в зависимости от настроек.
        /// </summary>
        /// <param name="optionsFactory"></param>
        public ApplicationDbContext(IDbContextOptionsFactory optionsFactory)
            : base(optionsFactory.CreateDbContextOptions())
        {
        }

        /// <summary>
        /// Не используется, Необходимо для неявного добавления типа Entity в контекст базы данных, иначе получаем исключение-> System.InvalidOperationException : Cannot create a DbSet for 'Entity' because this type is not included in the model for the context.
        /// В случае более продвинутой конфигурации можно добавлять в методе создания модели.
        /// </summary>
        DbSet<Entity> Entities { get; set; }

    }

}