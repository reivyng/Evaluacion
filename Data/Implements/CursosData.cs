﻿using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;

namespace Data.Implements.Security.RolData
{
    public class CursosData : BaseModelData<CursosData>, IcursosData
    {
        public CursosData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool active)
        {
            var rol = await _context.Set<Cursos>().FindAsync(id);
            if (rol == null)
                return false;

            rol.Status = active;
            _context.Entry(rol).Property(r => r.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePartial(Rol rol)
        {
            var existingRol = await _context.Roles.FindAsync(rol.Id);
            if (existingRol == null) return false;
            _context.Roles.Update(existingRol);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
