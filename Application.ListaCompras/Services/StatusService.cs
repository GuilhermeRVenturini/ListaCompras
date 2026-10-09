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

        // aqui faço a operação e trato o retorno antes de passar pra API
        public StatusService(IRepository<Status, int> repository)
        {
            _repository = repository;
        }

        // busca a lista toda; no usuário dá pra passar o nome pra filtrar
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

        // procura um registro pelo id, se não achar volta erro
        public async Task<ResultData<StatusResponseDto>> GetByIdAsync(int id)
        {

            var result = await _repository.GetByIdAsync(id);

            return MapResult(result);
        }

        // evita repetir o mesmo código pra transformar o resultado em DTO
        private static ResultData<StatusResponseDto> MapResult(ResultData<Status> result)
        {

            if (!result.IsSuccess || result.Data is null)
                return ResultData<StatusResponseDto>.Error(result.Message);

            return ResultData<StatusResponseDto>.Success(
                StatusMapping.ToResponseDto(result.Data));
        }
    }
}
