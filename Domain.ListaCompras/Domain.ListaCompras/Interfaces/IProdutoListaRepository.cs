using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;

namespace Domain.ListaCompras.Interfaces
{
    public interface IProdutoListaRepository
    {
        Task<ResultData<IEnumerable<ProdutoLista>>> GetAllAsync();
        Task<ResultData<ProdutoLista>> GetByIdAsync(int produtoId, int listaId);
        Task<ResultData<ProdutoLista>> CreateAsync(ProdutoLista entity);
        Task<ResultData<ProdutoLista>> UpdateAsync(ProdutoLista entity);
        Task<ResultData<ProdutoLista>> DeleteAsync(int produtoId, int listaId);
    }
}
