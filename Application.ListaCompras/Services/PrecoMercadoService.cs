using Application.ListaCompras.DTOs.PrecoMercado;
using Application.ListaCompras.Interfaces;
using Application.ListaCompras.Mappings;
using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;

namespace Application.ListaCompras.Services
{
    public sealed class PrecoMercadoService : IPrecoMercadoService
    {
        private readonly IPrecoMercadoRepository _repository;

        public PrecoMercadoService(IPrecoMercadoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultData<IEnumerable<PrecoMercadoResponseDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            if (!result.IsSuccess || result.Data is null)
                return ResultData<IEnumerable<PrecoMercadoResponseDto>>.Error(result.Message);

            var response = result.Data
                .Select(PrecoMercadoMapping.ToResponseDto)
                .ToList();

            return ResultData<IEnumerable<PrecoMercadoResponseDto>>.Success(response);
        }

        public async Task<ResultData<PrecoMercadoResponseDto>> GetByIdAsync(int produtoId, int mercadoId)
        {
            var result = await _repository.GetByIdAsync(produtoId, mercadoId);

            return MapResult(result);
        }

        public async Task<ResultData<PrecoMercadoResponseDto>> CreateAsync(PrecoMercadoRequestDto request)
        {
            var entity = PrecoMercadoMapping.ToEntity(request);
            var result = await _repository.CreateAsync(entity);

            return MapResult(result);
        }

        public async Task<ResultData<PrecoMercadoResponseDto>> UpdateAsync(int produtoId, int mercadoId, PrecoMercadoRequestDto request)
        {
            if (request.ProdutoId != produtoId || request.MercadoId != mercadoId)
                return ResultData<PrecoMercadoResponseDto>.Error("Os identificadores da rota devem ser iguais aos informados no request.");

            var existingResult = await _repository.GetByIdAsync(produtoId, mercadoId);

            if (!existingResult.IsSuccess || existingResult.Data is null)
                return ResultData<PrecoMercadoResponseDto>.Error(existingResult.Message);

            PrecoMercadoMapping.MapToEntity(request, existingResult.Data);

            var updateResult = await _repository.UpdateAsync(existingResult.Data);

            return MapResult(updateResult);
        }

        public async Task<ResultData<PrecoMercadoResponseDto>> DeleteAsync(int produtoId, int mercadoId)
        {
            var result = await _repository.DeleteAsync(produtoId, mercadoId);

            return MapResult(result);
        }

        private static ResultData<PrecoMercadoResponseDto> MapResult(ResultData<PrecoMercado> result)
        {
            if (!result.IsSuccess || result.Data is null)
                return ResultData<PrecoMercadoResponseDto>.Error(result.Message);

            return ResultData<PrecoMercadoResponseDto>.Success(
                PrecoMercadoMapping.ToResponseDto(result.Data));
        }
    }
}
