namespace Entity.Model
{
    public class Inscripciones : BaseEntity
    {
        public int EstudianteId { get; set; }
        public Estudiantes Estudiante { get; set; }
        public int CursoId { get; set; }
        public Cursos Curso { get; set; }
    }
}