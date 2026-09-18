using Application.ListaCompras.DTOs.Usuario;
using Application.ListaCompras.Interfaces;
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

        public async Task<ResultData<IEnumerable<UsuarioResponseDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            if (!result.IsSuccess || result.Data is null)
                return ResultData<IEnumerable<UsuarioResponseDto>>.Error(result.Message);

            var response = result.Data
                .Select(UsuarioMapping.ToResponseDto)
                .ToList();

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

        public async Task<ResultData<UsuarioResponseDto>> UpdateAsync(Guid id, UsuarioRequestDto request)
        {
            var existingResult = await _repository.GetByIdAsync(id);

            if (!existingResult.IsSuccess || existingResult.Data is null)
                return ResultData<UsuarioResponseDto>.Error(existingResult.Message);

            UsuarioMapping.MapToEntity(request, existingResult.Data);

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
