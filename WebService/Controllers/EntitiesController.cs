using Core;
using Microsoft.AspNetCore.Mvc;
using WebService.BLL.Core;
using WebService.BLL;

namespace WebService
{
    /// <summary>
    /// Контроллер для Entity.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class EntitiesController : ControllerBase
    {
        private readonly IEntityService _entityService;
        private readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public EntitiesController(IEntityService entityService)
        {
            _entityService = entityService;

        }

        [HttpPost("insert")]
        public async Task<IActionResult> InsertEntity([FromBody] Entity entity)
        {
            try
            {
                var addedEntity = await _entityService.AddEntityAsync(entity);
                return Ok($"Entity with ID \"{addedEntity.Id}\" added.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to insert entity");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetEntity(Guid id)
        {
            try
            {
                var entity = await _entityService.GetEntityAsync(id);
                return Ok(entity);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to retrieve entity");
                return StatusCode(500, "Internal server error");
            }
        }
    }


}
