using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;

namespace Data.Implements
{
    public class InscripcionesData : BaseData<Inscripciones>, IInscripcionesData
    {
        public InscripcionesData(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<bool> UpdatePartial(Inscripciones inscripciones)
        {
            var existingInscripcion = await _dbSet.FindAsync(inscripciones.Id);
            if (existingInscripcion == null) return false;
            // Actualizar solo los campos que se deseen modificar
            existingInscripcion.EstudianteId = inscripciones.EstudianteId;
            existingInscripcion.CursoId = inscripciones.CursoId;
            _context.Set<Inscripciones>().Update(existingInscripcion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
