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

        // aqui faço a operação e trato o retorno antes de passar pra API
        public PrecoService(IRepository<Preco, int> repository)
        {
            _repository = repository;
        }

        // busca a lista toda; no usuário dá pra passar o nome pra filtrar
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

        // procura um registro pelo id, se não achar volta erro
        public async Task<ResultData<PrecoResponseDto>> GetByIdAsync(int id)
        {

            var result = await _repository.GetByIdAsync(id);

            return MapResult(result);
        }

        // cria um registro novo e devolve o resultado
        public async Task<ResultData<PrecoResponseDto>> CreateAsync(PrecoRequestDto request)
        {

            var entity = PrecoMapping.ToEntity(request);

            var result = await _repository.CreateAsync(entity);

            return MapResult(result);
        }

        // acha o registro e atualiza os dados dele
        public async Task<ResultData<PrecoResponseDto>> UpdateAsync(int id, PrecoRequestDto request)
        {

            var existingResult = await _repository.GetByIdAsync(id);

            if (!existingResult.IsSuccess || existingResult.Data is null)
                return ResultData<PrecoResponseDto>.Error(existingResult.Message);

            PrecoMapping.MapToExistingEntity(request, existingResult.Data);

            var updateResult = await _repository.UpdateAsync(existingResult.Data);

            return MapResult(updateResult);
        }

        // remove o registro usando o id
        public async Task<ResultData<PrecoResponseDto>> DeleteAsync(int id)
        {

            var result = await _repository.DeleteAsync(id);

            return MapResult(result);
        }

        // evita repetir o mesmo código pra transformar o resultado em DTO
        private static ResultData<PrecoResponseDto> MapResult(ResultData<Preco> result)
        {

            if (!result.IsSuccess || result.Data is null)
                return ResultData<PrecoResponseDto>.Error(result.Message);

            return ResultData<PrecoResponseDto>.Success(
                PrecoMapping.ToResponseDto(result.Data));
        }
    }
}
