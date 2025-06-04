using Business.Interfaces;
using Entity.Dtos.InscripcionesDTO;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interface;

namespace Web.Controllers.Implements
{
    [Route("api/[controller]")]
    public class InscripcionesController : GenericController<InscripcionesDto, Inscripciones>, IInscripcionesController
    {
        private readonly IInscripcionesBusiness _inscripcionesBusiness;

        public InscripcionesController(IInscripcionesBusiness inscripcionesBusiness, ILogger<InscripcionesController> logger)
            : base(inscripcionesBusiness, logger)
        {
            _inscripcionesBusiness = inscripcionesBusiness;
        }

        protected override int GetEntityId(InscripcionesDto dto)
        {
            return dto.Id;
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePartial(int id, [FromBody] UpdateInscripcionesDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _inscripcionesBusiness.UpdatePartialAsync(dto);
                return Ok(new { Success = result });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Error de validación al actualizar parcialmente inscripción: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar parcialmente inscripción: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        
    }
}
