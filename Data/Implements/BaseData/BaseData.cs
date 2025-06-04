using Data.Implements.ABaseData;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Implements.BaseData
{
    public class BaseData <T> : ABaseData<T> where T : BaseEntity
    {
        public BaseData(ApplicationDbContext context) : base(context)
        {
        }

        // Implementación completa de los métodos abstractos
        public override async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                .Where(e => EF.Property<bool>(e, "Status"))
                .ToListAsync();
        }


        public override async Task<T> GetByIdAsync(int id)
        {
            // Solo trae la entidad si está activa (Status == true)
            return await _context.Set<T>()
                .Where(e => EF.Property<int>(e, "Id") == id && EF.Property<bool>(e, "Status"))
                .FirstOrDefaultAsync();
        }


        public override async Task<T> CreateAsync(T entity)
        {
            // Si la entidad tiene la propiedad 'Status', la establecemos en true
            var statusProp = typeof(T).GetProperty("Status");
            if (statusProp != null && statusProp.PropertyType == typeof(bool))
            {
                statusProp.SetValue(entity, true);
            }

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public override async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public override  async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

      
    }
}
