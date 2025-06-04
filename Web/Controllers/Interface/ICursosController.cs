using Microsoft.AspNetCore.Mvc;
using Entity.Model;
using Entity.Dtos.CursosDTO;


namespace Web.Controllers.Interface
{
    public interface ICursosController : IGenericController<CursosDto, Cursos>
    {
        Task<IActionResult> UpdatePartial(int id, UpdateCursosDto dto);
        Task<IActionResult> DeleteLogic(int id);
    }
}