using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;

namespace Application.ListaCompras.Interfaces
{
    public interface IMercadoService
    {
        Task<ResultData<IEnumerable<Mercado>>> GetAllAsync();

        Task<ResultData<Mercado>> GetByIdAsync(int id);

        Task<ResultData<Mercado>> CreateAsync(Mercado mercado);

        Task<ResultData<Mercado>> UpdateAsync(Mercado mercado);

        Task<ResultData<Mercado>> DeleteAsync(int id);
    }
}