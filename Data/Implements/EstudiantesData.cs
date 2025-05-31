using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;

namespace Data.Implements
{
    public class EstudiantesData : BaseData<Estudiantes>, IEstudiantesData
    {
        public EstudiantesData(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<bool> UpdatePartial(Estudiantes estudiantes)
        {
            var existingEstudiante = await _dbSet.FindAsync(estudiantes.Id);
            if (existingEstudiante == null) return false;
            // Actualizar solo los campos que se deseen modificar
            existingEstudiante.Name = estudiantes.Name;
            existingEstudiante.Edad = estudiantes.Edad;
            existingEstudiante.Email = estudiantes.Email;
            _context.Set<Estudiantes>().Update(existingEstudiante);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActiveAsync(int id, bool active)
        {
            var estudiantes = await _context.Set<Estudiantes>().FindAsync(id);
            if (estudiantes == null)
                return false;

            estudiantes.Status = active;
            _context.Entry(estudiantes).Property(r => r.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
