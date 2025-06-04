using Entity.Dtos.CursosDTO;
using Entity.Model;

namespace Business.Interfaces
{
    public interface ICursosBusiness : IBaseBusiness<Cursos, CursosDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de un book.
        /// </summary>
        /// <param name="dto">Objeto que contiene los datos actualizados del book, como nombre o estado.</param>
        ///<returns>True si la actualización fue exitosa; de lo contario false</returns>
        Task<bool> UpdatePartialAsync(UpdateCursosDto dto);
        Task<bool> ActiveAsync(ActiveCursosDto dto);
    }
}
