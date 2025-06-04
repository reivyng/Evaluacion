using Business.Interfaces;
using Entity.Dtos.EstudiantesDTO;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interface;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace Web.Controllers.Implements
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudianteController : GenericController<EstudiantesDto, Estudiantes>, IEstudiantesController
    {
        private readonly IEstudiantesBusiness _estudianteBusiness;

        public EstudianteController(IEstudiantesBusiness estudianteBusiness, ILogger<EstudianteController> logger)
            : base(estudianteBusiness, logger)
        {
            _estudianteBusiness = estudianteBusiness;
        }

        protected override int GetEntityId(EstudiantesDto dto)
        {
            return dto.Id;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePartial(int id,  [FromBody] UpdateEstudiantesDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _estudianteBusiness.UpdatePartialAsync(dto);
                return Ok(new { Success = result });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Error de validación al actualizar parcialmente estudiante: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar parcialmente estudiante: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("logic/{id}")]
        public async Task<IActionResult> DeleteLogic(int id)
        {
            try
            {
                var dto = new ActiveEstudiantesDto { Id = id, Status = false }; // Se inicializa la propiedad requerida 'Status'
                var result = await _estudianteBusiness.ActiveAsync(dto);

                if (!result)
                    return NotFound($"Estudiante con ID {id} no encontrado");

                return Ok(new { Success = true });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Error de validación al eliminar lógicamente estudiante: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar lógicamente estudiante con ID {id}: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}