using Entity.Dtos.BaseDTO;

namespace Entity.Dtos.EstudiantesDTO
{
    public class UpdateEstudiantesDto : BaseDto
    {
        public int Edad { get; set; }
        public string Email { get; set; }
    }
}
