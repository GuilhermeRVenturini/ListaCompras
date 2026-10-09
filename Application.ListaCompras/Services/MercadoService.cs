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

        // aqui faço a operação e trato o retorno antes de passar pra API
        public MercadoService(IRepository<Mercado, int> repository)
        {
            _repository = repository;
        }

        // busca a lista toda; no usuário dá pra passar o nome pra filtrar
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

        // procura um registro pelo id, se não achar volta erro
        public async Task<ResultData<MercadoResponseDto>> GetByIdAsync(int id)
        {

            var result = await _repository.GetByIdAsync(id);

            return MapResult(result);
        }

        // cria um registro novo e devolve o resultado
        public async Task<ResultData<MercadoResponseDto>> CreateAsync(MercadoRequestDto request)
        {

            var entity = MercadoMapping.ToEntity(request);

            var result = await _repository.CreateAsync(entity);

            return MapResult(result);
        }

        // acha o registro e atualiza os dados dele
        public async Task<ResultData<MercadoResponseDto>> UpdateAsync(int id, MercadoRequestDto request)
        {

            var existingResult = await _repository.GetByIdAsync(id);

            if (!existingResult.IsSuccess || existingResult.Data is null)
                return ResultData<MercadoResponseDto>.Error(existingResult.Message);

            MercadoMapping.MapToExistingEntity(request, existingResult.Data);

            var updateResult = await _repository.UpdateAsync(existingResult.Data);

            return MapResult(updateResult);
        }

        // remove o registro usando o id
        public async Task<ResultData<MercadoResponseDto>> DeleteAsync(int id)
        {

            var result = await _repository.DeleteAsync(id);

            return MapResult(result);
        }

        // evita repetir o mesmo código pra transformar o resultado em DTO
        private static ResultData<MercadoResponseDto> MapResult(ResultData<Mercado> result)
        {

            if (!result.IsSuccess || result.Data is null)
                return ResultData<MercadoResponseDto>.Error(result.Message);

            return ResultData<MercadoResponseDto>.Success(
                MercadoMapping.ToResponseDto(result.Data));
        }
    }
}
