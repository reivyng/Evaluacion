using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.EstudiantesDTO;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;
using ValidationException = FluentValidation.ValidationException;

namespace Business.Implements
{
    public class EstudiantesBusiness : BaseBusiness<Estudiantes, EstudiantesDto>, IEstudiantesBusiness
    {
        private readonly IEstudiantesData _estudiantesData;

        public EstudiantesBusiness(IEstudiantesData estudiantesData, IMapper mapper, ILogger<EstudiantesBusiness> logger)
            : base(estudiantesData, mapper, logger)
        {
            _estudiantesData = estudiantesData;
        }

        public async Task<bool> UpdatePartialAsync(UpdateEstudiantesDto dto)
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
        public async Task<bool> ActiveAsync(ActiveEstudiantesDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id");

            var exists = await _estudiantesData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("estudiante", dto.Id);

            return await _estudiantesData.ActiveAsync(dto.Id, dto.Status);
        }
    }
}
