using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.EstudiantesDTO;
using Entity.Model;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;
using ValidationException = FluentValidation.ValidationException;

namespace Business.Implements
{
    public class EstudiantesBusiness : BaseBusiness<Estudiantes, EstudiantesDto>, IEstudiantesBusiness
    {
        private readonly IEstudiantesData _estudiantesData;
        private readonly IValidator<EstudiantesDto> _validator;

        public EstudiantesBusiness(IEstudiantesData estudiantesData, IMapper mapper, ILogger<EstudiantesBusiness> logger, IValidator<EstudiantesDto> validator)
            : base(estudiantesData, mapper, logger, validator)
        {
            _estudiantesData = estudiantesData;
            _validator = validator;
        }

        public async Task<bool> UpdatePartialAuthorAsync(UpdateEstudiantesDto dto)
        {
            if (dto.Id <= 0)
                throw new ArgumentException("ID inválido.");
            var estudiante = _mapper.Map<Estudiantes>(dto);
            var result = await _estudiantesData.UpdatePartial(estudiante);
            return result;
        }

        ///<summary>
        /// Desactiva un estudiante en la base de datos
        /// </summary>
        public async Task<bool> DeleteLogicEstudianteAsync(ActiveEstudiantesDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id");

            var exists = await _estudiantesData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("estudiante", dto.Id);

            return await _estudiantesData.ActiveAsync(dto.Id, dto.Status);
        }
    }
}
