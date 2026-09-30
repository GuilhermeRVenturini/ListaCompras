using Application.ListaCompras.DTOs.Historico;
using Domain.ListaCompras.Common.Results;

namespace Application.ListaCompras.Interfaces
{
    public interface IHistoricoService
    {
        Task<ResultData<IEnumerable<HistoricoResponseDto>>> GetAllAsync();
        Task<ResultData<HistoricoResponseDto>> GetByIdAsync(int id);
    }
}
