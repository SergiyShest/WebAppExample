using Microsoft.EntityFrameworkCore;
using WebService.DAL.Core;

namespace WebService.DAL.OptionsFactory
{
    /// <summary>
    /// В зависимости от реализации позволяет менять базу данных используемую в приложении 
    /// </summary>
    public interface IDbContextOptionsFactory
    {
        /// <summary>
        /// Создает объект DbContextOptions для ApplicationDbContext. в котором содержатся указания какую базу данных использовать.
        /// </summary>
        DbContextOptions<ApplicationDbContext> CreateDbContextOptions();
    }
}
