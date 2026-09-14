using Domain.ListaCompras.Common.Results;

namespace Domain.ListaCompras.Interfaces
{
    public interface ICreateRepository<T>
        where T : class
    {
        Task<ResultData<T>> CreateAsync(T entity);
    }
}
