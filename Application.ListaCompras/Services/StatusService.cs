using Application.ListaCompras.DTOs.Status;
using Application.ListaCompras.Interfaces;
using Application.ListaCompras.Mappings;
using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;

namespace Application.ListaCompras.Services
{
    public sealed class StatusService : IStatusService
    {
        private readonly IRepository<Status, int> _repository;

        public StatusService(IRepository<Status, int> repository)
        {
            _repository = repository;
        }

        public async Task<ResultData<IEnumerable<StatusResponseDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            if (!result.IsSuccess || result.Data is null)
                return ResultData<IEnumerable<StatusResponseDto>>.Error(result.Message);

            var response = result.Data
                .Select(StatusMapping.ToResponseDto)
                .ToList();

            return ResultData<IEnumerable<StatusResponseDto>>.Success(response);
        }

        public async Task<ResultData<StatusResponseDto>> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);

            return MapResult(result);
        }

        public async Task<ResultData<StatusResponseDto>> CreateAsync(StatusRequestDto request)
        {
            var entity = StatusMapping.ToEntity(request);
            var result = await _repository.CreateAsync(entity);

            return MapResult(result);
        }

        public async Task<ResultData<StatusResponseDto>> UpdateAsync(int id, StatusRequestDto request)
        {
            var existingResult = await _repository.GetByIdAsync(id);

            if (!existingResult.IsSuccess || existingResult.Data is null)
                return ResultData<StatusResponseDto>.Error(existingResult.Message);

            StatusMapping.MapToEntity(request, existingResult.Data);

            var updateResult = await _repository.UpdateAsync(existingResult.Data);

            return MapResult(updateResult);
        }

        public async Task<ResultData<StatusResponseDto>> DeleteAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);

            return MapResult(result);
        }

        private static ResultData<StatusResponseDto> MapResult(ResultData<Status> result)
        {
            if (!result.IsSuccess || result.Data is null)
                return ResultData<StatusResponseDto>.Error(result.Message);

            return ResultData<StatusResponseDto>.Success(
                StatusMapping.ToResponseDto(result.Data));
        }
    }
}
