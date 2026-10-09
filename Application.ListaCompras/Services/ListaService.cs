using Application.ListaCompras.DTOs.Lista;
using Application.ListaCompras.Interfaces;
using Application.ListaCompras.Mappings;
using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;
using Domain.ListaCompras.Interfaces;

// service de Lista: onde deixei a lógica das operações antes de chamar o repository
namespace Application.ListaCompras.Services
{

    public sealed class ListaService : IListaService
    {

        private readonly IRepository<Lista, int> _repository;

        // aqui faço a operação e trato o retorno antes de passar pra API
        public ListaService(IRepository<Lista, int> repository)
        {
            _repository = repository;
        }

        // busca a lista toda; no usuário dá pra passar o nome pra filtrar
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

        // procura um registro pelo id, se não achar volta erro
        public async Task<ResultData<ListaResponseDto>> GetByIdAsync(int id)
        {

            var result = await _repository.GetByIdAsync(id);

            return MapResult(result);
        }

        // cria um registro novo e devolve o resultado
        public async Task<ResultData<ListaResponseDto>> CreateAsync(ListaRequestDto request)
        {

            var entity = ListaMapping.ToEntity(request);

            var result = await _repository.CreateAsync(entity);

            return MapResult(result);
        }

        // acha o registro e atualiza os dados dele
        public async Task<ResultData<ListaResponseDto>> UpdateAsync(int id, ListaRequestDto request)
        {

            var existingResult = await _repository.GetByIdAsync(id);

            if (!existingResult.IsSuccess || existingResult.Data is null)
                return ResultData<ListaResponseDto>.Error(existingResult.Message);

            ListaMapping.MapToExistingEntity(request, existingResult.Data);

            var updateResult = await _repository.UpdateAsync(existingResult.Data);

            return MapResult(updateResult);
        }

        // remove o registro usando o id
        public async Task<ResultData<ListaResponseDto>> DeleteAsync(int id)
        {

            var result = await _repository.DeleteAsync(id);

            return MapResult(result);
        }

        // evita repetir o mesmo código pra transformar o resultado em DTO
        private static ResultData<ListaResponseDto> MapResult(ResultData<Lista> result)
        {

            if (!result.IsSuccess || result.Data is null)
                return ResultData<ListaResponseDto>.Error(result.Message);

            return ResultData<ListaResponseDto>.Success(
                ListaMapping.ToResponseDto(result.Data));
        }
    }
}
