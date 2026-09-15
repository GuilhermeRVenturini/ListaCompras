using Domain.ListaCompras.Interfaces;

namespace Domain.ListaCompras.Interfaces
{
    public interface IRepository<T, TId> :
        IReadRepository<T, TId>,
        ICreateRepository<T>,
        IUpdateRepository<T>,
        IDeleteRepository<T, TId>
        where T : class
    {
    }
}