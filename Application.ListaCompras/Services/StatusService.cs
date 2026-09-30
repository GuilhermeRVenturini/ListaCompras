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

        private static ResultData<StatusResponseDto> MapResult(ResultData<Status> result)
        {
            if (!result.IsSuccess || result.Data is null)
                return ResultData<StatusResponseDto>.Error(result.Message);

            return ResultData<StatusResponseDto>.Success(
                StatusMapping.ToResponseDto(result.Data));
        }
    }
}
