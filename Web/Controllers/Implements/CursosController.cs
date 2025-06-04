using Business.Interfaces;
using Entity.Dtos.CursosDTO;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interface;

namespace Web.Controllers.Implements
{
    [Route("api/[controller]")]
    public class CursosController : GenericController<CursosDto, Cursos>, ICursosController
    {
        private readonly ICursosBusiness _cursosBusiness;

        public CursosController(ICursosBusiness cursosBusiness, ILogger<CursosController> logger)
            : base(cursosBusiness, logger)
        {
            _cursosBusiness = cursosBusiness;
        }

        protected override int GetEntityId(CursosDto dto)
        {
            return dto.Id;
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePartial(int id, [FromBody] UpdateCursosDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _cursosBusiness.UpdatePartialAsync(dto);
                return Ok(new { Success = result });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Error de validación al actualizar parcialmente curso: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar parcialmente curso: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("logic/{id}")]
        public async Task<IActionResult> DeleteLogic(int id)
        {
            try
            {
                var dto = new ActiveCursosDto { Id = id, Status = false };
                var result = await _cursosBusiness.ActiveAsync(dto);

                if (!result)
                    return NotFound($"Curso con ID {id} no encontrado");

                return Ok(new { Success = true });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Error de validación al eliminar lógicamente curso: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar lógicamente curso con ID {id}: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
