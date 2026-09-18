using Application.ListaCompras.DTOs.Produto;
using Application.ListaCompras.Interfaces;
using Application.ListaCompras.Mappings;
using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;

namespace Application.ListaCompras.Services
{
    public sealed class ProdutoService : IProdutoService
    {
        private readonly IRepository<Produto, int> _repository;

        public ProdutoService(IRepository<Produto, int> repository)
        {
            _repository = repository;
        }

        public async Task<ResultData<IEnumerable<ProdutoResponseDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            if (!result.IsSuccess || result.Data is null)
                return ResultData<IEnumerable<ProdutoResponseDto>>.Error(result.Message);

            var response = result.Data
                .Select(ProdutoMapping.ToResponseDto)
                .ToList();

            return ResultData<IEnumerable<ProdutoResponseDto>>.Success(response);
        }

        public async Task<ResultData<ProdutoResponseDto>> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);

            return MapResult(result);
        }

        public async Task<ResultData<ProdutoResponseDto>> CreateAsync(ProdutoRequestDto request)
        {
            var entity = ProdutoMapping.ToEntity(request);
            var result = await _repository.CreateAsync(entity);

            return MapResult(result);
        }

        public async Task<ResultData<ProdutoResponseDto>> UpdateAsync(int id, ProdutoRequestDto request)
        {
            var existingResult = await _repository.GetByIdAsync(id);

            if (!existingResult.IsSuccess || existingResult.Data is null)
                return ResultData<ProdutoResponseDto>.Error(existingResult.Message);

            ProdutoMapping.MapToEntity(request, existingResult.Data);

            var updateResult = await _repository.UpdateAsync(existingResult.Data);

            return MapResult(updateResult);
        }

        public async Task<ResultData<ProdutoResponseDto>> DeleteAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);

            return MapResult(result);
        }

        private static ResultData<ProdutoResponseDto> MapResult(ResultData<Produto> result)
        {
            if (!result.IsSuccess || result.Data is null)
                return ResultData<ProdutoResponseDto>.Error(result.Message);

            return ResultData<ProdutoResponseDto>.Success(
                ProdutoMapping.ToResponseDto(result.Data));
        }
    }
}
