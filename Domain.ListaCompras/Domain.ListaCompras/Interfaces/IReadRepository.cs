using Domain.ListaCompras.Common.Results;
using System.Linq.Expressions;

namespace Domain.ListaCompras.Interfaces
{
    public interface IReadRepository<T, TId>
        where T : class
    {
        Task<ResultData<IEnumerable<T>>> GetAllAsync(Expression<Func<T, bool>>? filtro = null);

        Task<ResultData<T>> GetByIdAsync(TId id);
    }
}
