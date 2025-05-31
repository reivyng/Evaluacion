namespace Entity.Dtos
{
    public class EstudiantesDto : GenericDto
    {
        public int Edad { get; set; }
        public string Email { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
