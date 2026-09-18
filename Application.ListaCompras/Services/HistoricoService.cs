using Application.ListaCompras.DTOs.Historico;
using Application.ListaCompras.Interfaces;
using Application.ListaCompras.Mappings;
using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;

namespace Application.ListaCompras.Services
{
    public sealed class HistoricoService : IHistoricoService
    {
        private readonly IRepository<Historico, int> _repository;

        public HistoricoService(IRepository<Historico, int> repository)
        {
            _repository = repository;
        }

        public async Task<ResultData<IEnumerable<HistoricoResponseDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            if (!result.IsSuccess || result.Data is null)
                return ResultData<IEnumerable<HistoricoResponseDto>>.Error(result.Message);

            var response = result.Data
                .Select(HistoricoMapping.ToResponseDto)
                .ToList();

            return ResultData<IEnumerable<HistoricoResponseDto>>.Success(response);
        }

        public async Task<ResultData<HistoricoResponseDto>> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);

            return MapResult(result);
        }

        public async Task<ResultData<HistoricoResponseDto>> CreateAsync(HistoricoRequestDto request)
        {
            var entity = HistoricoMapping.ToEntity(request);
            var result = await _repository.CreateAsync(entity);

            return MapResult(result);
        }

        public async Task<ResultData<HistoricoResponseDto>> UpdateAsync(int id, HistoricoRequestDto request)
        {
            var existingResult = await _repository.GetByIdAsync(id);

            if (!existingResult.IsSuccess || existingResult.Data is null)
                return ResultData<HistoricoResponseDto>.Error(existingResult.Message);

            HistoricoMapping.MapToEntity(request, existingResult.Data);

            var updateResult = await _repository.UpdateAsync(existingResult.Data);

            return MapResult(updateResult);
        }

        public async Task<ResultData<HistoricoResponseDto>> DeleteAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);

            return MapResult(result);
        }

        private static ResultData<HistoricoResponseDto> MapResult(ResultData<Historico> result)
        {
            if (!result.IsSuccess || result.Data is null)
                return ResultData<HistoricoResponseDto>.Error(result.Message);

            return ResultData<HistoricoResponseDto>.Success(
                HistoricoMapping.ToResponseDto(result.Data));
        }
    }
}
