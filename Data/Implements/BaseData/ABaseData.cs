﻿using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Implements.ABaseData
{

    /// <summary>
    /// Clase abstracta para poder sobre escribir métodos e incluir nuevos metodos sin cambiar la Interfaz
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ABaseData<T> : IBaseData<T> where T : BaseEntity
    {
    
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected ABaseData(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // Métodos abstractos que deben ser implementados por las clases derivadas
        public abstract Task<List<T>> GetAllAsync();
        public abstract Task<T> GetByIdAsync(int id);
        public abstract Task<T> CreateAsync(T entity);
        public abstract Task<T> UpdateAsync(T entity);
        public abstract Task<bool> DeleteAsync(int id);

    }
}
