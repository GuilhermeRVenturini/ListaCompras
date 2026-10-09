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

        // aqui faço a operação e trato o retorno antes de passar pra API
        public HistoricoService(IRepository<Historico, int> repository)
        {
            _repository = repository;
        }

        // busca a lista toda; no usuário dá pra passar o nome pra filtrar
        public async Task<ResultData<IEnumerable<HistoricoResponseDto>>> GetAllAsync()
        {

            var result = await _repository.GetAllAsync();

            if (!result.IsSuccess || result.Data is null)
                return ResultData<IEnumerable<HistoricoResponseDto>>.Error(result.Message);

            var response = result.Data
                .OrderByDescending(x => x.DataRegistro)

                .Select(HistoricoMapping.ToResponseDto)
                .ToList();

            return ResultData<IEnumerable<HistoricoResponseDto>>.Success(response);
        }

        // procura um registro pelo id, se não achar volta erro
        public async Task<ResultData<HistoricoResponseDto>> GetByIdAsync(int id)
        {

            var result = await _repository.GetByIdAsync(id);

            if (!result.IsSuccess || result.Data is null)
                return ResultData<HistoricoResponseDto>.Error(result.Message);

            return ResultData<HistoricoResponseDto>.Success(
                HistoricoMapping.ToResponseDto(result.Data));
        }
    }
}
