using Domain.ListaCompras.Common.Results;

namespace Domain.ListaCompras.Interfaces
{
    public interface IDeleteRepository<T, TId>
        where T : class
    {
        Task<ResultData<T>> DeleteAsync(TId id);
    }
}