namespace Entity.Model
{
    public class Cursos : GenericModel
    {
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Profesor { get; set; }

        public ICollection<Inscripciones> Inscripciones { get; set; }
    }
}
