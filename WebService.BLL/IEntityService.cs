using Core;
using WebService.BLL.Core;

namespace WebService.BLL
{
    public interface IEntityService : IGenericService<Entity>
    {
        Task<Entity> AddEntityAsync(Entity entity);
        Task<Entity> GetEntityAsync(Guid id);
    }

}


