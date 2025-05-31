using Microsoft.AspNetCore.Mvc;
using Entity.Model;
using Entity.Dtos.InscripcionesDTO;


namespace Web.Controllers.Interface
{
    public interface IInscripcionesController : IGenericController<InscripcionesDto, Inscripciones>
    {
        Task<IActionResult> UpdatePartial(int id, int authorId, UpdateInscripcionesDto dto);
    }
}