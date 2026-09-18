using Application.ListaCompras.DTOs.Mercado;
using Application.ListaCompras.Interfaces;
using Application.ListaCompras.Mappings;
using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;

namespace Application.ListaCompras.Services
{
    public sealed class MercadoService : IMercadoService
    {
        private readonly IRepository<Mercado, int> _repository;

        public MercadoService(IRepository<Mercado, int> repository)
        {
            _repository = repository;
        }

        public async Task<ResultData<IEnumerable<MercadoResponseDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            if (!result.IsSuccess || result.Data is null)
                return ResultData<IEnumerable<MercadoResponseDto>>.Error(result.Message);

            var response = result.Data
                .Select(MercadoMapping.ToResponseDto)
                .ToList();

            return ResultData<IEnumerable<MercadoResponseDto>>.Success(response);
        }

        public async Task<ResultData<MercadoResponseDto>> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);

            return MapResult(result);
        }

        public async Task<ResultData<MercadoResponseDto>> CreateAsync(MercadoRequestDto request)
        {
            var entity = MercadoMapping.ToEntity(request);
            var result = await _repository.CreateAsync(entity);

            return MapResult(result);
        }

        public async Task<ResultData<MercadoResponseDto>> UpdateAsync(int id, MercadoRequestDto request)
        {
            var existingResult = await _repository.GetByIdAsync(id);

            if (!existingResult.IsSuccess || existingResult.Data is null)
                return ResultData<MercadoResponseDto>.Error(existingResult.Message);

            MercadoMapping.MapToEntity(request, existingResult.Data);

            var updateResult = await _repository.UpdateAsync(existingResult.Data);

            return MapResult(updateResult);
        }

        public async Task<ResultData<MercadoResponseDto>> DeleteAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);

            return MapResult(result);
        }

        private static ResultData<MercadoResponseDto> MapResult(ResultData<Mercado> result)
        {
            if (!result.IsSuccess || result.Data is null)
                return ResultData<MercadoResponseDto>.Error(result.Message);

            return ResultData<MercadoResponseDto>.Success(
                MercadoMapping.ToResponseDto(result.Data));
        }
    }
}
