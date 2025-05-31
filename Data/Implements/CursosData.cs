using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;

namespace Data.Implements
{
    public class CursosData : BaseData<Cursos>, IcursosData
    {
        public CursosData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> UpdatePartial(Cursos cursos)
        {
            var existingCursos = await _dbSet.FindAsync(cursos.Id);
            if (existingCursos == null) return false;
            // Actualizar solo los campos que se deseen modificar
            existingCursos.Name = cursos.Name;
            existingCursos.Descripcion = cursos.Descripcion;
            existingCursos.Profesor = cursos.Profesor;
            existingCursos.FechaFin = cursos.FechaFin;
            _context.Set<Cursos>().Update(existingCursos);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActiveAsync(int id, bool active)
        {
            var cursos = await _context.Set<Cursos>().FindAsync(id);
            if (cursos == null)
                return false;

            cursos.Status = active;
            _context.Entry(cursos).Property(r => r.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
