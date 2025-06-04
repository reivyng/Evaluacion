using Microsoft.AspNetCore.Mvc;
using Entity.Model;
using Entity.Dtos.EstudiantesDTO;


namespace Web.Controllers.Interface
{
    public interface IEstudiantesController : IGenericController<EstudiantesDto, Estudiantes>
    {
        Task<IActionResult> UpdatePartial(int id, UpdateEstudiantesDto dto);
        Task<IActionResult> DeleteLogic(int id);
    }
}