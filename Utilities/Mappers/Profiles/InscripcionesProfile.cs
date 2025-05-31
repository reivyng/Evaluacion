using AutoMapper;
using Entity.Dtos.InscripcionesDTO;
using Entity.Model;

namespace Utilities.Mappers.Profiles
{
    public class InscripcionesProfile : Profile
    {
        public InscripcionesProfile()
        {
            CreateMap<Inscripciones, InscripcionesDto>().ReverseMap();
            CreateMap<Inscripciones, UpdateInscripcionesDto>().ReverseMap();
        }
    }
}
