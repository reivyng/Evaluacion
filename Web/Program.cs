using System.Text;
using Microsoft.EntityFrameworkCore;
using Business.Interfaces;
using Business.Implements;
using FluentValidation;
using Entity.Context;
using Data.Interfaces;
using Data.Implements;
using Data.Implements.BaseData;
using Utilities.Mappers.Profiles;
using Web.ServiceExtension;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));
builder.Services.AddSingleton<IValidatorFactory>(sp =>
    new ServiceProviderValidatorFactory(sp));

// Swagger
builder.Services.AddSwaggerDocumentation();

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register generic repositories and business logic
builder.Services.AddScoped(typeof(IBaseData<>), typeof(BaseData<>));
builder.Services.AddScoped(typeof(IBaseBusiness<,>), typeof(BaseBusiness<,>));

// Register Curso-specific services
builder.Services.AddScoped<IcursosData, CursosData>();
builder.Services.AddScoped<ICursosBusiness, CursosBusiness>();

// Register Estudiante-specific services
builder.Services.AddScoped<IEstudiantesData, EstudiantesData>();
builder.Services.AddScoped<IEstudiantesBusiness, EstudiantesBusiness>();

// Register Inscripcion-specific services
builder.Services.AddScoped<IInscripcionesData, InscripcionesData>();
builder.Services.AddScoped<IInscripcionesBusiness, InscripcionesBusiness>();

builder.Services.AddAutoMapper(typeof(CursosProfile));
builder.Services.AddAutoMapper(typeof(EstudiantesProfile));
builder.Services.AddAutoMapper(typeof(InscripcionesProfile));



var app = builder.Build();

// Swagger (solo en desarrollo)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Sistema de Gestión v1");
        c.RoutePrefix = string.Empty;
    });
}

// Usa la política de CORS registrada en ApplicationServiceExtensions
app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();

// Autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Inicializar base de datos y aplicar migraciones
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        dbContext.Database.Migrate();
        logger.LogInformation("Base de datos verificada y migraciones aplicadas exitosamente.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error durante la migración de la base de datos.");
    }
}

app.Run();