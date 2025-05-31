using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.InscripcionesDTO;
using Entity.Model;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Business.Implements
{
    public class InscripcionesBusiness : BaseBusiness<Inscripciones, InscripcionesDto>, IInscripcionesBusiness
    {
        private readonly IInscripcionesData _inscripcionesData;
        private readonly IValidator<InscripcionesDto> _validator;

        public InscripcionesBusiness(
            IInscripcionesData inscripcionesData,
            IMapper mapper,
            ILogger<InscripcionesBusiness> logger,
            IValidator<InscripcionesDto> validator)
            : base(inscripcionesData, mapper, logger, validator)
        {
            _inscripcionesData = inscripcionesData;
            _validator = validator;
        }

        public async Task<bool> UpdatePartialAsync(UpdateInscripcionesDto dto)
        {
            if (dto.Id <= 0)
                throw new ArgumentException("ID inválido.");
            var inscripcion = _mapper.Map<Inscripciones>(dto);
            var result = await _inscripcionesData.UpdatePartial(inscripcion);
            return result;
        }
    }
}
