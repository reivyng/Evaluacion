using AutoMapper;
using Entity.Dtos.CursosDTO;
using Entity.Dtos.EstudiantesDTO;
using Entity.Dtos.InscripcionesDTO;
using Entity.Model;

namespace Utilities.Mappers.Profiles
{
    public class EstudiantesProfile : Profile
    {
        public EstudiantesProfile()
        {
            CreateMap<Estudiantes, EstudiantesDto>().ReverseMap();
            CreateMap<Estudiantes, UpdateEstudiantesDto>().ReverseMap();
            CreateMap<Estudiantes, ActiveEstudiantesDto>().ReverseMap();
        }
    }
}
