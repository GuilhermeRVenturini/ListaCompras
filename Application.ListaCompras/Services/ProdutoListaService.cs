using Application.ListaCompras.DTOs.ProdutoLista;
using Application.ListaCompras.Interfaces;
using Application.ListaCompras.Mappings;
using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;

namespace Application.ListaCompras.Services
{
    public sealed class ProdutoListaService : IProdutoListaService
    {
        private readonly IProdutoListaRepository _repository;

        public ProdutoListaService(IProdutoListaRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultData<IEnumerable<ProdutoListaResponseDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            if (!result.IsSuccess || result.Data is null)
                return ResultData<IEnumerable<ProdutoListaResponseDto>>.Error(result.Message);

            var response = result.Data
                .Select(ProdutoListaMapping.ToResponseDto)
                .ToList();

            return ResultData<IEnumerable<ProdutoListaResponseDto>>.Success(response);
        }

        public async Task<ResultData<ProdutoListaResponseDto>> GetByIdAsync(int produtoId, int listaId)
        {
            var result = await _repository.GetByIdAsync(produtoId, listaId);

            return MapResult(result);
        }

        public async Task<ResultData<ProdutoListaResponseDto>> CreateAsync(ProdutoListaRequestDto request)
        {
            var entity = ProdutoListaMapping.ToEntity(request);
            var result = await _repository.CreateAsync(entity);

            return MapResult(result);
        }

        public async Task<ResultData<ProdutoListaResponseDto>> UpdateAsync(int produtoId, int listaId, ProdutoListaRequestDto request)
        {
            if (request.ProdutoId != produtoId || request.ListaId != listaId)
                return ResultData<ProdutoListaResponseDto>.Error("Os identificadores da rota devem ser iguais aos informados no request.");

            var existingResult = await _repository.GetByIdAsync(produtoId, listaId);

            if (!existingResult.IsSuccess || existingResult.Data is null)
                return ResultData<ProdutoListaResponseDto>.Error(existingResult.Message);

            ProdutoListaMapping.MapToEntity(request, existingResult.Data);

            var updateResult = await _repository.UpdateAsync(existingResult.Data);

            return MapResult(updateResult);
        }

        public async Task<ResultData<ProdutoListaResponseDto>> DeleteAsync(int produtoId, int listaId)
        {
            var result = await _repository.DeleteAsync(produtoId, listaId);

            return MapResult(result);
        }

        private static ResultData<ProdutoListaResponseDto> MapResult(ResultData<ProdutoLista> result)
        {
            if (!result.IsSuccess || result.Data is null)
                return ResultData<ProdutoListaResponseDto>.Error(result.Message);

            return ResultData<ProdutoListaResponseDto>.Success(
                ProdutoListaMapping.ToResponseDto(result.Data));
        }
    }
}
