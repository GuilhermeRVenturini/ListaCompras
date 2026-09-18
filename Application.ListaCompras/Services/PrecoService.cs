using Application.ListaCompras.DTOs.Preco;
using Application.ListaCompras.Interfaces;
using Application.ListaCompras.Mappings;
using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;

namespace Application.ListaCompras.Services
{
    public sealed class PrecoService : IPrecoService
    {
        private readonly IRepository<Preco, int> _repository;

        public PrecoService(IRepository<Preco, int> repository)
        {
            _repository = repository;
        }

        public async Task<ResultData<IEnumerable<PrecoResponseDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            if (!result.IsSuccess || result.Data is null)
                return ResultData<IEnumerable<PrecoResponseDto>>.Error(result.Message);

            var response = result.Data
                .Select(PrecoMapping.ToResponseDto)
                .ToList();

            return ResultData<IEnumerable<PrecoResponseDto>>.Success(response);
        }

        public async Task<ResultData<PrecoResponseDto>> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);

            return MapResult(result);
        }

        public async Task<ResultData<PrecoResponseDto>> CreateAsync(PrecoRequestDto request)
        {
            var entity = PrecoMapping.ToEntity(request);
            var result = await _repository.CreateAsync(entity);

            return MapResult(result);
        }

        public async Task<ResultData<PrecoResponseDto>> UpdateAsync(int id, PrecoRequestDto request)
        {
            var existingResult = await _repository.GetByIdAsync(id);

            if (!existingResult.IsSuccess || existingResult.Data is null)
                return ResultData<PrecoResponseDto>.Error(existingResult.Message);

            PrecoMapping.MapToEntity(request, existingResult.Data);

            var updateResult = await _repository.UpdateAsync(existingResult.Data);

            return MapResult(updateResult);
        }

        public async Task<ResultData<PrecoResponseDto>> DeleteAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);

            return MapResult(result);
        }

        private static ResultData<PrecoResponseDto> MapResult(ResultData<Preco> result)
        {
            if (!result.IsSuccess || result.Data is null)
                return ResultData<PrecoResponseDto>.Error(result.Message);

            return ResultData<PrecoResponseDto>.Success(
                PrecoMapping.ToResponseDto(result.Data));
        }
    }
}
