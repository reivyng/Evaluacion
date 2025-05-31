using Entity.Dtos.BaseDTO;

namespace Entity.Dtos.CursosDTO
{
    public class CursosDto : GenericDto
    {
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Profesor { get; set; }
    }
}
