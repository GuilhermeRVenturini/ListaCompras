using Application.ListaCompras.DTOs.Historico;
using Domain.ListaCompras.Common.Results;

namespace Application.ListaCompras.Interfaces
{
    public interface IHistoricoService
    {
        Task<ResultData<IEnumerable<HistoricoResponseDto>>> GetAllAsync();

        Task<ResultData<HistoricoResponseDto>> GetByIdAsync(int id);

        Task<ResultData<HistoricoResponseDto>> CreateAsync(HistoricoRequestDto request);

        Task<ResultData<HistoricoResponseDto>> UpdateAsync(int id, HistoricoRequestDto request);

        Task<ResultData<HistoricoResponseDto>> DeleteAsync(int id);
    }
}
