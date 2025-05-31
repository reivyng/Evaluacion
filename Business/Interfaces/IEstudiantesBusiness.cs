using Entity.Dtos.EstudiantesDTO;
using Entity.Model;

namespace Business.Interfaces
{
    public interface IEstudiantesBusiness : IBaseBusiness<Estudiantes, EstudiantesDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de un book.
        /// </summary>
        /// <param name="dto">Objeto que contiene los datos actualizados del book, como nombre o estado.</param>
        ///<returns>True si la actualización fue exitosa; de lo contario false</returns>
        Task<bool> UpdatePartialAsync(UpdateEstudiantesDto dto);
        Task<bool> ActiveAsync(ActiveEstudiantesDto dto);
    }
}
