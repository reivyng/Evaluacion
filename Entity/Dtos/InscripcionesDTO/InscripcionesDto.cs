using Entity.Dtos.BaseDTO;

namespace Entity.Dtos.InscripcionesDTO
{
    public class InscripcionesDto : BaseDto
    {
        public int EstudianteId { get; set; }
        public int CursoId { get; set; }
    }
}
