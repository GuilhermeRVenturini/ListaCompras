using Application.ListaCompras.DTOs.UsuarioLista;
using Domain.ListaCompras.Common.Results;

namespace Application.ListaCompras.Interfaces
{
    public interface IUsuarioListaService
    {
        Task<ResultData<IEnumerable<UsuarioListaResponseDto>>> GetAllAsync();

        Task<ResultData<UsuarioListaResponseDto>> GetByIdAsync(int listaId, Guid usuarioId);

        Task<ResultData<UsuarioListaResponseDto>> CreateAsync(UsuarioListaRequestDto request);

        Task<ResultData<UsuarioListaResponseDto>> DeleteAsync(int listaId, Guid usuarioId);
    }
}
