using Entity.Dtos.BaseDTO;

namespace Entity.Dtos.CursosDTO
{
    public class UpdateCursosDto : BaseDto
    {
        public string Descripcion { get; set; }
        public DateTime FechaFin { get; set; }
        public string Profesor { get; set; }
    }
}
