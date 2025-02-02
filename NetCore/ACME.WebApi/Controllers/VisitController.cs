namespace ACME.WebApi.Controllers
{
    using ACME.BL;
    using ACME.DataAccess.Repositories;
    using ACME.Dtos;
    using Microsoft.AspNetCore.Mvc;
    [ApiController]
    [Route("api/[controller]")]
    public class VisitController : ControllerBase
    {
        private readonly IVisitService _visitService;

        private readonly ILogger<VisitController> _logger;

        private readonly VisitJsonRepository _visitJsonRepository;
        public VisitController(IVisitService visitService, VisitJsonRepository visitJsonRepository, ILogger<VisitController> logger)
        {
            _visitService = visitService;
            _visitJsonRepository = visitJsonRepository;
            _logger = logger;
        }
        /// <summary>
        /// Obtiene todas las visitas registradas.
        /// </summary>
        /// <returns>Lista de todas las visitas.</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _visitService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller error. Method: GetAllAsync()");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Acciones sobre el JSON.
        /// Obtiene todas las visitas registradas.
        /// </summary>
        /// <returns>Lista de todas las visitas.</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFromJsonAsync()
        {
            try
            {
                var result = await _visitJsonRepository.GetAllFromJsonAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller error. Method: GetFromJsonAsync()");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Obtiene todas las visitas que no han sido eliminadas.
        /// </summary>
        /// <returns>Lista de visitas no eliminadas.</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllNotDeletedAsync()
        {
            try
            {
                var result = await _visitService.GetAllNotDeletedAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller error. Method: GetAllNotDeletedAsync()");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Obtiene una visita por su ID.
        /// </summary>
        /// <param name="id">Identificador único de la visita.</param>
        /// <returns>La visita correspondiente al ID proporcionado.</returns>
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _visitService.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller error. Method: GetByIdAsync()");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Agrega una nueva visita.
        /// </summary>
        /// <param name="visitDto">Objeto DTO de la visita a agregar.</param>
        /// <returns>La visita agregada.</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync([FromBody] VisitDto visitDto)
        {
            if (visitDto == null)
            {
                return BadRequest("Invalid visit data");
            }
            try
            {
                var result = await _visitService.AddAsync(visitDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller error. Method: AddAsync()");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Acciones sobre el JSON.
        /// Agrega una nueva visita.
        /// </summary>
        /// <param name="visitDto">Objeto DTO de la visita a agregar.</param>
        /// <returns>La visita agregada.</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> AddToJsonAsync([FromBody] VisitDto visitDto)
        {
            try
            {
                await _visitJsonRepository.AddToJsonAsync(visitDto);
                return Ok("Visit added to JSON");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller error. Method: AddToJsonAsync()");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Actualiza una visita existente.
        /// </summary>
        /// <param name="id">Identificador único de la visita.</param>
        /// <param name="visitDto">Objeto DTO con la información actualizada.</param>
        /// <returns>La visita actualizada.</returns>
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] VisitDto visitDto)
        {
            if (visitDto == null)
            {
                return BadRequest("Invalid visit data");
            }
            try
            {
                var result = await _visitService.UpdateAsync(visitDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller error. Method: UpdateAsync()");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Acciones sobre el JSON.
        /// Actualiza una visita existente.
        /// </summary>
        /// <param name="visitDto">Objeto DTO con la información actualizada.</param>
        /// <returns>La visita actualizada.</returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateInJsonAsync([FromBody] VisitDto visitDto)
        {
            try
            {
                await _visitJsonRepository.UpdateInJsonAsync(visitDto);
                return Ok("Visit updated in JSON");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller error. Method: UpdateInJsonAsync()");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Elimina una visita por su ID.
        /// </summary>
        /// <param name="id">Identificador único de la visita a eliminar.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _visitService.DeleteByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller error. Method: DeleteByIdAsync()");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Acciones sobre el JSON.
        /// Elimina una visita por su ID.
        /// </summary>
        /// <param name="id">Identificador único de la visita a eliminar.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteFromJson(Guid id)
        {
            try
            {
                await _visitJsonRepository.DeleteFromJsonAsync(id);
                return Ok("Visit deleted from JSON");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller error. Method: DeleteFromJson()");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
