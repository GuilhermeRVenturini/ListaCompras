using Application.ListaCompras.DTOs.Usuario;
using Domain.ListaCompras.Common.Results;

namespace Application.ListaCompras.Interfaces
{

    public interface IUsuarioService
    {

        Task<ResultData<IEnumerable<UsuarioResponseDto>>> GetAllAsync(UsuarioFiltroDto filtro);

        Task<ResultData<UsuarioResponseDto>> GetByIdAsync(Guid id);

        Task<ResultData<UsuarioResponseDto>> CreateAsync(UsuarioRequestDto request);

        Task<ResultData<UsuarioResponseDto>> UpdateAsync(Guid id, UsuarioPatchDto request);

        Task<ResultData<UsuarioResponseDto>> DeleteAsync(Guid id);
    }
}
