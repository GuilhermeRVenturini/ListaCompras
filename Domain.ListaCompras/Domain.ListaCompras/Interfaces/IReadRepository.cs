using Domain.ListaCompras.Common.Results;

namespace Domain.ListaCompras.Interfaces
{
    public interface IReadRepository<T, TId>
        where T : class
    {
        Task<ResultData<IEnumerable<T>>> GetAllAsync();

        Task<ResultData<T>> GetByIdAsync(TId id);
    }
}
