using Domain.ListaCompras.Common.Results;

namespace Domain.ListaCompras.Interfaces
{
    public interface IUpdateRepository<T>
        where T : class
    {
        Task<ResultData<T>> UpdateAsync(T entity);
    }
}
