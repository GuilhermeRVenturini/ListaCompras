using Application.ListaCompras.DTOs.Lista;
using Application.ListaCompras.Interfaces;
using Application.ListaCompras.Mappings;
using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;

namespace Application.ListaCompras.Services
{
    public sealed class ListaService : IListaService
    {
        private readonly IRepository<Lista, int> _repository;

        public ListaService(IRepository<Lista, int> repository)
        {
            _repository = repository;
        }

        public async Task<ResultData<IEnumerable<ListaResponseDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            if (!result.IsSuccess || result.Data is null)
                return ResultData<IEnumerable<ListaResponseDto>>.Error(result.Message);

            var response = result.Data
                .Select(ListaMapping.ToResponseDto)
                .ToList();

            return ResultData<IEnumerable<ListaResponseDto>>.Success(response);
        }

        public async Task<ResultData<ListaResponseDto>> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);

            return MapResult(result);
        }

        public async Task<ResultData<ListaResponseDto>> CreateAsync(ListaRequestDto request)
        {
            var entity = ListaMapping.ToEntity(request);
            var result = await _repository.CreateAsync(entity);

            return MapResult(result);
        }

        public async Task<ResultData<ListaResponseDto>> UpdateAsync(int id, ListaRequestDto request)
        {
            var existingResult = await _repository.GetByIdAsync(id);

            if (!existingResult.IsSuccess || existingResult.Data is null)
                return ResultData<ListaResponseDto>.Error(existingResult.Message);

            ListaMapping.MapToEntity(request, existingResult.Data);

            var updateResult = await _repository.UpdateAsync(existingResult.Data);

            return MapResult(updateResult);
        }

        public async Task<ResultData<ListaResponseDto>> DeleteAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);

            return MapResult(result);
        }

        private static ResultData<ListaResponseDto> MapResult(ResultData<Lista> result)
        {
            if (!result.IsSuccess || result.Data is null)
                return ResultData<ListaResponseDto>.Error(result.Message);

            return ResultData<ListaResponseDto>.Success(
                ListaMapping.ToResponseDto(result.Data));
        }
    }
}
