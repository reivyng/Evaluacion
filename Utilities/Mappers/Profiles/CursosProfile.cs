using AutoMapper;
using Entity.Dtos.CursosDTO;
using Entity.Dtos.InscripcionesDTO;
using Entity.Model;

namespace Utilities.Mappers.Profiles
{
    public class CursosProfile : Profile
    {
        public CursosProfile()
        {
            CreateMap<Cursos, CursosDto>().ReverseMap();
            CreateMap<Cursos, UpdateCursosDto>().ReverseMap();
            CreateMap<Cursos, ActiveCursosDto>().ReverseMap();
        }
    }
}
