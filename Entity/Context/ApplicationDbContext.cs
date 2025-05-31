using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using Dapper;
using System.Linq.Expressions;
using Entity.Model;

namespace Entity.Context
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        // DbSet properties
        public DbSet<Estudiantes> Estudiantes { get; set; }
        public DbSet<Cursos> Cursos { get; set; }
        public DbSet<Inscripciones> Inscripciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la clave compuesta para la tabla pivote
            modelBuilder.Entity<Inscripciones>()
                .HasKey(i => new { i.EstudianteId, i.CursoId });

            // Relación Estudiantes-Inscripciones
            modelBuilder.Entity<Inscripciones>()
                .HasOne(i => i.Estudiante)
                .WithMany(e => e.Inscripciones)
                .HasForeignKey(i => i.EstudianteId);

            // Relación Cursos-Inscripciones
            modelBuilder.Entity<Inscripciones>()
                .HasOne(i => i.Curso)
                .WithMany(c => c.Inscripciones)
                .HasForeignKey(i => i.CursoId);
        }

        /// <summary>
        /// Configura opciones adicionales del contexto, como el registro de datos sensibles.
        /// </summary>
        /// <param name="optionsBuilder">Constructor de opciones de configuración del contexto.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            // Otras configuraciones adicionales pueden ir aquí
        }

        /// <summary>
        /// Configura convenciones de tipos de datos, estableciendo la precisión por defecto de los valores decimales.
        /// </summary>
        /// <param name="configurationBuilder">Constructor de configuración de modelos.</param>
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }

        /// <summary>
        /// Ejecuta una consulta SQL utilizando Dapper y devuelve una colección de resultados de tipo genérico.
        /// </summary>
        public async Task<IEnumerable<T>> QueryAsync<T>(string text, object? parameters = null, int? timeout = null, CommandType? type = null)
        {
            using var command = new DapperEFCoreCommand(this, text, parameters ?? new { }, timeout, type, CancellationToken.None);
            var connection = this.Database.GetDbConnection();
            return await connection.QueryAsync<T>(command.Definition);
        }

        /// <summary>
        /// Ejecuta una consulta SQL utilizando Dapper y devuelve un solo resultado o el valor predeterminado si no hay resultados.
        /// </summary>
        public async Task<T?> QueryFirstOrDefaultAsync<T>(string text, object? parameters = null, int? timeout = null, CommandType? type = null)
        {
            using var command = new DapperEFCoreCommand(this, text, parameters ?? new { }, timeout, type, CancellationToken.None);
            var connection = this.Database.GetDbConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(command.Definition);
        }

        /// <summary>
        /// Obtiene un IQueryable para usar en consultas LINQ que incluye filtro de status activo.
        /// </summary>
        public IQueryable<T> GetActiveSet<T>() where T : class
        {
            var query = Set<T>().AsQueryable();

            // Filtramos por Status aplicando expresiones genéricas si la entidad tiene la propiedad Status
            var parameter = Expression.Parameter(typeof(T), "e");

            if (typeof(T).GetProperty("Status") != null)
            {
                try
                {
                    // Construimos una expresión lambda para filtrar por Status = true
                    var property = Expression.Property(parameter, "Status");
                    var value = Expression.Constant(true);
                    var equal = Expression.Equal(property, value);
                    var lambda = Expression.Lambda<Func<T, bool>>(equal, parameter);

                    // Aplicamos el filtro
                    query = query.Where(lambda);
                }
                catch
                {
                    // Si hay algún error, devolvemos el query sin filtrar
                }
            }

            return query;
        }

        /// <summary>
        /// Método auxiliar para obtener el valor de una propiedad de un objeto mediante reflexión.
        /// </summary>
        private static bool GetPropertyValue(object obj, string propertyName)
        {
            var property = obj.GetType().GetProperty(propertyName);
            if (property == null)
            {
                return false;
            }
            return property.GetValue(obj, null) is bool value ? value : false;
        }

        /// <summary>
        /// Ejecuta una consulta con paginación utilizando LINQ.
        /// </summary>
        public IQueryable<T> GetPaged<T>(IQueryable<T> query, int page, int pageSize) where T : class
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Ejecuta una consulta LINQ y devuelve los resultados como una colección asíncrona.
        /// </summary>
        public async Task<List<T>> ToListAsyncSafe<T>(IQueryable<T> query)
        {
            if (query == null)
                return new List<T>();

            return await EntityFrameworkQueryableExtensions.ToListAsync(query);
        }

        /// <summary>
        /// Estructura para ejecutar comandos SQL con Dapper en Entity Framework Core.
        /// </summary>
        public readonly struct DapperEFCoreCommand : IDisposable
        {
            public DapperEFCoreCommand(DbContext context, string text, object parameters, int? timeout, CommandType? type, CancellationToken ct)
            {
                var transaction = context.Database.CurrentTransaction?.GetDbTransaction();
                var commandType = type ?? CommandType.Text;
                var commandTimeout = timeout ?? context.Database.GetCommandTimeout() ?? 30;

                Definition = new CommandDefinition(
                    text,
                    parameters,
                    transaction,
                    commandTimeout,
                    commandType,
                    cancellationToken: ct
                );
            }

            public CommandDefinition Definition { get; }

            public void Dispose()
            {
            }
        }
    }
}
