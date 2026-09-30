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
        private readonly IRepository<Produto, int> _produtoRepository;
        private readonly IRepository<Mercado, int> _mercadoRepository;
        private readonly IRepository<Preco, int> _precoRepository;

        public PrecoMercadoService(
            IPrecoMercadoRepository repository,
            IRepository<Produto, int> produtoRepository,
            IRepository<Mercado, int> mercadoRepository,
            IRepository<Preco, int> precoRepository)
        {
            _repository = repository;
            _produtoRepository = produtoRepository;
            _mercadoRepository = mercadoRepository;
            _precoRepository = precoRepository;
        }

        public async Task<ResultData<IEnumerable<PrecoMercadoResponseDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            return MapCollectionResult(result);
        }

        public async Task<ResultData<IEnumerable<PrecoMercadoResponseDto>>> GetByProdutoAsync(int produtoId)
        {
            var produtoResult = await _produtoRepository.GetByIdAsync(produtoId);

            if (!produtoResult.IsSuccess || produtoResult.Data is null)
                return ResultData<IEnumerable<PrecoMercadoResponseDto>>.Error("Produto não encontrado.");

            var result = await _repository.GetByProdutoIdAsync(produtoId);
            return MapCollectionResult(result);
        }

        public async Task<ResultData<IEnumerable<PrecoMercadoResponseDto>>> GetByMercadoAsync(int mercadoId)
        {
            var mercadoResult = await _mercadoRepository.GetByIdAsync(mercadoId);

            if (!mercadoResult.IsSuccess || mercadoResult.Data is null)
                return ResultData<IEnumerable<PrecoMercadoResponseDto>>.Error("Mercado não encontrado.");

            var result = await _repository.GetByMercadoIdAsync(mercadoId);
            return MapCollectionResult(result);
        }

        public async Task<ResultData<PrecoMercadoResponseDto>> GetByIdAsync(int produtoId, int mercadoId)
        {
            var result = await _repository.GetByIdAsync(produtoId, mercadoId);
            return MapResult(result);
        }

        public async Task<ResultData<PrecoMercadoResponseDto>> CreateAsync(
            int produtoId,
            int mercadoId,
            PrecoMercadoRequestDto request)
        {
            var produtoResult = await _produtoRepository.GetByIdAsync(produtoId);
            if (!produtoResult.IsSuccess || produtoResult.Data is null)
                return ResultData<PrecoMercadoResponseDto>.Error("Produto não encontrado.");

            var mercadoResult = await _mercadoRepository.GetByIdAsync(mercadoId);
            if (!mercadoResult.IsSuccess || mercadoResult.Data is null)
                return ResultData<PrecoMercadoResponseDto>.Error("Mercado não encontrado.");

            var precoResult = await _precoRepository.GetByIdAsync(request.PrecoId);
            if (!precoResult.IsSuccess || precoResult.Data is null)
                return ResultData<PrecoMercadoResponseDto>.Error("Preço não encontrado.");

            var existingResult = await _repository.GetByIdAsync(produtoId, mercadoId);
            if (existingResult.IsSuccess && existingResult.Data is not null)
                return ResultData<PrecoMercadoResponseDto>.Error("Este produto já possui um preço vinculado a este mercado.");

            var entity = PrecoMercadoMapping.ToEntity(produtoId, mercadoId, request);
            entity.Produto = produtoResult.Data;
            entity.Mercado = mercadoResult.Data;
            entity.Preco = precoResult.Data;

            var result = await _repository.CreateAsync(entity);
            return MapResult(result);
        }

        public async Task<ResultData<PrecoMercadoResponseDto>> UpdateAsync(
            int produtoId,
            int mercadoId,
            PrecoMercadoRequestDto request)
        {
            var existingResult = await _repository.GetByIdAsync(produtoId, mercadoId);

            if (!existingResult.IsSuccess || existingResult.Data is null)
                return ResultData<PrecoMercadoResponseDto>.Error("Preço do produto neste mercado não encontrado.");

            var precoResult = await _precoRepository.GetByIdAsync(request.PrecoId);
            if (!precoResult.IsSuccess || precoResult.Data is null)
                return ResultData<PrecoMercadoResponseDto>.Error("Preço não encontrado.");

            PrecoMercadoMapping.MapToEntity(request, existingResult.Data);
            existingResult.Data.Preco = precoResult.Data;

            var updateResult = await _repository.UpdateAsync(existingResult.Data);
            return MapResult(updateResult);
        }

        public async Task<ResultData<PrecoMercadoResponseDto>> DeleteAsync(int produtoId, int mercadoId)
        {
            var result = await _repository.DeleteAsync(produtoId, mercadoId);
            return MapResult(result);
        }

        private static ResultData<IEnumerable<PrecoMercadoResponseDto>> MapCollectionResult(
            ResultData<IEnumerable<PrecoMercado>> result)
        {
            if (!result.IsSuccess || result.Data is null)
                return ResultData<IEnumerable<PrecoMercadoResponseDto>>.Error(result.Message);

            var response = result.Data
                .Select(PrecoMercadoMapping.ToResponseDto)
                .ToList();

            return ResultData<IEnumerable<PrecoMercadoResponseDto>>.Success(response);
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
