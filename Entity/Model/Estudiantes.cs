namespace Entity.Model
{
    public class Estudiantes : GenericModel
    {
        public int Edad { get; set; }
        public string Email { get; set; }
        public DateTime FechaRegistro { get; set; }

        public ICollection<Inscripciones> Inscripciones { get; set; }
    }
}
