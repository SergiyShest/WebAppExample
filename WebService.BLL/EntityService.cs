using Core;
using DAL.Core;
using Microsoft.EntityFrameworkCore;
using WebService.BLL.Core;
using WebService.DAL.Core;

namespace WebService.BLL
{

    public class EntityService : GenericService<Entity>, IEntityService
    {
        public EntityService(IUnitOfWork uow) : base(uow) { }
    }

}


