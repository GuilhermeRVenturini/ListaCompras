using Domain.ListaCompras.Common.Results;
using Domain.ListaCompras.Entities;

namespace Domain.ListaCompras.Interfaces
{
    public interface IUsuarioListaRepository
    {
        Task<ResultData<IEnumerable<UsuarioLista>>> GetAllAsync();
        Task<ResultData<IEnumerable<UsuarioLista>>> GetByListaIdAsync(int listaId);
        Task<ResultData<IEnumerable<UsuarioLista>>> GetByUsuarioIdAsync(Guid usuarioId);
        Task<ResultData<UsuarioLista>> GetByIdAsync(int listaId, Guid usuarioId);
        Task<ResultData<UsuarioLista>> CreateAsync(UsuarioLista entity);
        Task<ResultData<UsuarioLista>> DeleteAsync(int listaId, Guid usuarioId);
    }
}
