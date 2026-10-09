using Application.ListaCompras.DTOs.Usuario;
using Application.ListaCompras.Interfaces;
using Application.ListaCompras.Validation;
using Application.ListaCompras.Filters;
using Application.ListaCompras.Mappings;
using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;

namespace Application.ListaCompras.Services
{

    public sealed class UsuarioService : IUsuarioService
    {

        private readonly IRepository<Usuario, Guid> _repository;

        public UsuarioService(IRepository<Usuario, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<ResultData<IEnumerable<UsuarioResponseDto>>> GetAllAsync(UsuarioFiltroDto filtro)
        {
            var expressao = new UsuarioFilter { Nome = filtro?.Nome, Cpf = filtro?.Cpf }
                .ToExpression();

            var result = await _repository.GetAllAsync(expressao);

            if (!result.IsSuccess || result.Data is null)
                return ResultData<IEnumerable<UsuarioResponseDto>>.Error(result.Message);

            var response = result.Data.Select(UsuarioMapping.ToResponseDto).ToList();
            return ResultData<IEnumerable<UsuarioResponseDto>>.Success(response);
        }

        public async Task<ResultData<UsuarioResponseDto>> GetByIdAsync(Guid id)
        {

            var result = await _repository.GetByIdAsync(id);

            return MapResult(result);
        }

        public async Task<ResultData<UsuarioResponseDto>> CreateAsync(UsuarioRequestDto request)
        {

            var entity = UsuarioMapping.ToEntity(request);

            var result = await _repository.CreateAsync(entity);

            return MapResult(result);
        }

        public async Task<ResultData<UsuarioResponseDto>> UpdateAsync(Guid id, UsuarioPatchDto request)
        {
            var erroPatch = PatchValidator.Validate(request);
            if (erroPatch is not null)
                return ResultData<UsuarioResponseDto>.Error(erroPatch);

            var existingResult = await _repository.GetByIdAsync(id);

            if (!existingResult.IsSuccess || existingResult.Data is null)
                return ResultData<UsuarioResponseDto>.Error(existingResult.Message);

            UsuarioMapping.MapToExistingEntity(request, existingResult.Data);

            var updateResult = await _repository.UpdateAsync(existingResult.Data);

            return MapResult(updateResult);
        }

        public async Task<ResultData<UsuarioResponseDto>> DeleteAsync(Guid id)
        {

            var result = await _repository.DeleteAsync(id);

            return MapResult(result);
        }

        private static ResultData<UsuarioResponseDto> MapResult(ResultData<Usuario> result)
        {

            if (!result.IsSuccess || result.Data is null)
                return ResultData<UsuarioResponseDto>.Error(result.Message);

            return ResultData<UsuarioResponseDto>.Success(
                UsuarioMapping.ToResponseDto(result.Data));
        }
    }
}
