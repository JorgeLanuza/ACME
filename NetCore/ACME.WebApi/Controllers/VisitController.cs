namespace ACME.WebApi.Controllers
{
    using ACME.BL;
    using ACME.BL.Services;
    using ACME.Dtos;
    using Microsoft.AspNetCore.Mvc;
    [ApiController]
    [Route("api/[controller]")]
    public class VisitController : ControllerBase
    {
        private readonly IVisitService _visitService;

        private readonly ILogger<VisitController> _logger;
        public VisitController(IVisitService visitService, ILogger<VisitController> logger)
        {
            _visitService = visitService;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _visitService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllNotDeletedAsync()
        {
            var result = await _visitService.GetAllNotDeletedAsync();
            return Ok(result);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _visitService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync([FromBody] VisitDto visitDto)
        {
            if (visitDto == null)
            {
                return BadRequest("Invalid visit data");
            }
            var result = await _visitService.AddAsync(visitDto);
            return Ok(result);
        }
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] VisitDto visitDto)
        {
            if (visitDto == null)
            {
                return BadRequest("Invalid visit data");
            }
            var result = await _visitService.UpdateAsync(visitDto);
            return Ok(result);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _visitService.DeleteByIdAsync(id);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ImportFromExcelAsync(IFormFile file)
        {
            return Ok("Datos importados desde el archivo Excel");
        }
    }
}
