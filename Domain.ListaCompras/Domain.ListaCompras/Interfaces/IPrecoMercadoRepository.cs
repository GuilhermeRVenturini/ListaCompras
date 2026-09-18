using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;

namespace Domain.ListaCompras.Interfaces
{
    public interface IPrecoMercadoRepository
    {
        Task<ResultData<IEnumerable<PrecoMercado>>> GetAllAsync();
        Task<ResultData<PrecoMercado>> GetByIdAsync(int produtoId, int mercadoId);
        Task<ResultData<PrecoMercado>> CreateAsync(PrecoMercado entity);
        Task<ResultData<PrecoMercado>> UpdateAsync(PrecoMercado entity);
        Task<ResultData<PrecoMercado>> DeleteAsync(int produtoId, int mercadoId);
    }
}
