using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.CursosDTO;
using Entity.Model;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;
using ValidationException = FluentValidation.ValidationException;

namespace Business.Implements
{
    public class CursosBusiness : BaseBusiness<Cursos, CursosDto>, ICursosBusiness
    {
        private readonly IcursosData _cursosData;
        private readonly IValidator<CursosDto> _validator;

        public CursosBusiness(IcursosData cursosData, IMapper mapper, ILogger<CursosBusiness> logger, IValidator<CursosDto> validator)
            : base(cursosData, mapper, logger, validator)
        {
            _cursosData = cursosData;
            _validator = validator;
        }

        public async Task<bool> UpdatePartialAuthorAsync(UpdateCursosDto dto)
        {
            if (dto.Id <= 0)
                throw new ArgumentException("ID inválido.");
            var cursos = _mapper.Map<Cursos>(dto);
            var result = await _cursosData.UpdatePartial(cursos);
            return result;
        }

        ///<summary>
        /// Desactiva un rol en la base de datos
        /// </summary>
        public async Task<bool> DeleteLogicRolAsync(ActiveCursosDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id");

            var exists = await _cursosData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("rol", dto.Id);

            return await _cursosData.ActiveAsync(dto.Id, dto.Status);
        }
    }
}
