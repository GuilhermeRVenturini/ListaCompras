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
        private readonly IRepository<Lista, int> _listaRepository;
        private readonly IRepository<Produto, int> _produtoRepository;
        private readonly IRepository<Status, int> _statusRepository;

        public ProdutoListaService(
            IProdutoListaRepository repository,
            IRepository<Lista, int> listaRepository,
            IRepository<Produto, int> produtoRepository,
            IRepository<Status, int> statusRepository)
        {
            _repository = repository;
            _listaRepository = listaRepository;
            _produtoRepository = produtoRepository;
            _statusRepository = statusRepository;
        }

        public async Task<ResultData<IEnumerable<ProdutoListaResponseDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            return MapCollectionResult(result);
        }

        public async Task<ResultData<IEnumerable<ProdutoListaResponseDto>>> GetByListaAsync(int listaId)
        {
            var listaResult = await _listaRepository.GetByIdAsync(listaId);

            if (!listaResult.IsSuccess || listaResult.Data is null)
                return ResultData<IEnumerable<ProdutoListaResponseDto>>.Error("Lista não encontrada.");

            var result = await _repository.GetByListaIdAsync(listaId);
            return MapCollectionResult(result);
        }

        public async Task<ResultData<IEnumerable<ProdutoListaResponseDto>>> GetByProdutoAsync(int produtoId)
        {
            var produtoResult = await _produtoRepository.GetByIdAsync(produtoId);

            if (!produtoResult.IsSuccess || produtoResult.Data is null)
                return ResultData<IEnumerable<ProdutoListaResponseDto>>.Error("Produto não encontrado.");

            var result = await _repository.GetByProdutoIdAsync(produtoId);
            return MapCollectionResult(result);
        }

        public async Task<ResultData<ProdutoListaResponseDto>> GetByIdAsync(int produtoId, int listaId)
        {
            var result = await _repository.GetByIdAsync(produtoId, listaId);
            return MapResult(result);
        }

        public async Task<ResultData<ProdutoListaResponseDto>> CreateAsync(
            int listaId,
            int produtoId,
            ProdutoListaRequestDto request)
        {
            var listaResult = await _listaRepository.GetByIdAsync(listaId);
            if (!listaResult.IsSuccess || listaResult.Data is null)
                return ResultData<ProdutoListaResponseDto>.Error("Lista não encontrada.");

            var produtoResult = await _produtoRepository.GetByIdAsync(produtoId);
            if (!produtoResult.IsSuccess || produtoResult.Data is null)
                return ResultData<ProdutoListaResponseDto>.Error("Produto não encontrado.");

            var statusResult = await _statusRepository.GetByIdAsync(request.StatusId);
            if (!statusResult.IsSuccess || statusResult.Data is null)
                return ResultData<ProdutoListaResponseDto>.Error("Status não encontrado.");

            var existingResult = await _repository.GetByIdAsync(produtoId, listaId);
            if (existingResult.IsSuccess && existingResult.Data is not null)
                return ResultData<ProdutoListaResponseDto>.Error("O produto já está adicionado a esta lista.");

            var entity = ProdutoListaMapping.ToEntity(produtoId, listaId, request);
            entity.Lista = listaResult.Data;
            entity.Produto = produtoResult.Data;
            entity.Status = statusResult.Data;

            var result = await _repository.CreateAsync(entity);
            return MapResult(result);
        }

        public async Task<ResultData<ProdutoListaResponseDto>> UpdateAsync(
            int listaId,
            int produtoId,
            ProdutoListaRequestDto request)
        {
            var existingResult = await _repository.GetByIdAsync(produtoId, listaId);

            if (!existingResult.IsSuccess || existingResult.Data is null)
                return ResultData<ProdutoListaResponseDto>.Error("Produto não encontrado nesta lista.");

            var statusResult = await _statusRepository.GetByIdAsync(request.StatusId);
            if (!statusResult.IsSuccess || statusResult.Data is null)
                return ResultData<ProdutoListaResponseDto>.Error("Status não encontrado.");

            ProdutoListaMapping.MapToEntity(request, existingResult.Data);
            existingResult.Data.Status = statusResult.Data;

            var updateResult = await _repository.UpdateAsync(existingResult.Data);
            return MapResult(updateResult);
        }

        public async Task<ResultData<ProdutoListaResponseDto>> DeleteAsync(int produtoId, int listaId)
        {
            var result = await _repository.DeleteAsync(produtoId, listaId);
            return MapResult(result);
        }

        private static ResultData<IEnumerable<ProdutoListaResponseDto>> MapCollectionResult(
            ResultData<IEnumerable<ProdutoLista>> result)
        {
            if (!result.IsSuccess || result.Data is null)
                return ResultData<IEnumerable<ProdutoListaResponseDto>>.Error(result.Message);

            var response = result.Data
                .Select(ProdutoListaMapping.ToResponseDto)
                .ToList();

            return ResultData<IEnumerable<ProdutoListaResponseDto>>.Success(response);
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
