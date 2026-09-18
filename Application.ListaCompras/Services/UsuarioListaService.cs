using Application.ListaCompras.DTOs.UsuarioLista;
using Application.ListaCompras.Interfaces;
using Application.ListaCompras.Mappings;
using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;

namespace Application.ListaCompras.Services
{
    public sealed class UsuarioListaService : IUsuarioListaService
    {
        private readonly IUsuarioListaRepository _repository;

        public UsuarioListaService(IUsuarioListaRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultData<IEnumerable<UsuarioListaResponseDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            if (!result.IsSuccess || result.Data is null)
                return ResultData<IEnumerable<UsuarioListaResponseDto>>.Error(result.Message);

            var response = result.Data
                .Select(UsuarioListaMapping.ToResponseDto)
                .ToList();

            return ResultData<IEnumerable<UsuarioListaResponseDto>>.Success(response);
        }

        public async Task<ResultData<UsuarioListaResponseDto>> GetByIdAsync(int listaId, Guid usuarioId)
        {
            var result = await _repository.GetByIdAsync(listaId, usuarioId);

            return MapResult(result);
        }

        public async Task<ResultData<UsuarioListaResponseDto>> CreateAsync(UsuarioListaRequestDto request)
        {
            var entity = UsuarioListaMapping.ToEntity(request);
            var result = await _repository.CreateAsync(entity);

            return MapResult(result);
        }

        public async Task<ResultData<UsuarioListaResponseDto>> DeleteAsync(int listaId, Guid usuarioId)
        {
            var result = await _repository.DeleteAsync(listaId, usuarioId);

            return MapResult(result);
        }

        private static ResultData<UsuarioListaResponseDto> MapResult(ResultData<UsuarioLista> result)
        {
            if (!result.IsSuccess || result.Data is null)
                return ResultData<UsuarioListaResponseDto>.Error(result.Message);

            return ResultData<UsuarioListaResponseDto>.Success(
                UsuarioListaMapping.ToResponseDto(result.Data));
        }
    }
}
