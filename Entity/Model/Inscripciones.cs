namespace Entity.Model
{
    public class Inscripciones : GenericModel
    {
        public int EstudiantesId { get; set; }
        public Estudiantes Estudiante { get; set; }
        public int CursosId { get; set; }
        public Cursos Curso { get; set; }
    }
}