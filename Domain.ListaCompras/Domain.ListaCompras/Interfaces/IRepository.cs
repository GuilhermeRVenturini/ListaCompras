using Domain.ListaCompras.Interfaces;

// contrato do repository de dados: só diz o que precisa ter, não mexe no banco aqui
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
