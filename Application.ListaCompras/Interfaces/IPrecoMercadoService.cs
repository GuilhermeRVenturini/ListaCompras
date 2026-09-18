using Application.ListaCompras.DTOs.PrecoMercado;
using Domain.ListaCompras.Common.Results;

namespace Application.ListaCompras.Interfaces
{
    public interface IPrecoMercadoService
    {
        Task<ResultData<IEnumerable<PrecoMercadoResponseDto>>> GetAllAsync();

        Task<ResultData<PrecoMercadoResponseDto>> GetByIdAsync(int produtoId, int mercadoId);

        Task<ResultData<PrecoMercadoResponseDto>> CreateAsync(PrecoMercadoRequestDto request);

        Task<ResultData<PrecoMercadoResponseDto>> UpdateAsync(int produtoId, int mercadoId, PrecoMercadoRequestDto request);

        Task<ResultData<PrecoMercadoResponseDto>> DeleteAsync(int produtoId, int mercadoId);
    }
}
