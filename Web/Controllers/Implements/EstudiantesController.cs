using Entity.Dtos.EstudiantesDTO;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interface;

namespace Web.Controllers.Implements;

[Route("api/[controller]")]
public class EstudiantesController : GenericController<EstudiantesDto, Estudiantes>, IEstudiantesController
{
    private readonly IEstudiantesController _estudiantesController;
    public EstudiantesController(IEstudiantesController estudiantesBusiness, ILogger<EstudiantesController> logger)
        : base(estudiantesBusiness, logger)
    {
        _estudiantesBusiness = estudiantesBusiness;
    }

    protected override int GetEntityId(EstudiantesDto dto)
    {
        return dto.Id;
    }

    [HttpPatch]

    public async Task<IActionResult> UpdatePartial(int id, int authorId, [FromBody] UpdateEstudiantesDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _estudiantesBusiness.UpdatePartialAuthorAsync(dto);
            return Ok(new { Success = result });
        }
        catch (ArgumentException ex)
        {
            _logger.LogError($"Error de validación al actualizar parcialmente el autor: {ex.Message}");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al actualizar parcialmente el autor : {ex.Message}");
            return StatusCode(500, "Error interno del servidor");
        }
    }


}