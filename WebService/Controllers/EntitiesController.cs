using Domain;
using Microsoft.AspNetCore.Mvc;
using WebService.BLL;
namespace WebService
{
    [ApiController]
    [Route("[controller]")]
    public class EntitiesController : ControllerBase
    {

        IEntityService<Entity> _service;
        public EntitiesController(IEntityService<Entity> service)
        {

            _service = service;
        }


        [HttpPost("insert")]
        public async Task<IActionResult> InsertEntity([FromBody] Entity entity)
        {
            // Проверяем, существует ли уже сущность с таким ID
            var existingEntity = await _service.FindAsync(entity.Id);
            if (existingEntity == null)
            {
               
                var addedEntity = await _service.AddAsync(entity);
                return Ok($"Entity with ID \"{addedEntity.Id}\" added.");
            }
            else
            {
                //    Если сущность существует,можно обновить ее данные, но так как в задании ни чего не сказано про это, то верну ошибку
                return BadRequest($"Entity with ID \"{entity.Id}\" already exists!");
 
            }
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetEntity(Guid id)
        {
            var existingEntity = await _service.FindAsync(id);
            if (existingEntity != null)
            {
                 return Ok(existingEntity);
            }
            else
            {
                return NotFound($"Entity with ID {id} not found.");
            }
        }
    }
}
