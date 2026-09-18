using Application.ListaCompras.DTOs.Mercado;
using Domain.ListaCompras.Common.Results;

namespace Application.ListaCompras.Interfaces
{
    public interface IMercadoService
    {
        Task<ResultData<IEnumerable<MercadoResponseDto>>> GetAllAsync();

        Task<ResultData<MercadoResponseDto>> GetByIdAsync(int id);

        Task<ResultData<MercadoResponseDto>> CreateAsync(MercadoRequestDto request);

        Task<ResultData<MercadoResponseDto>> UpdateAsync(int id, MercadoRequestDto request);

        Task<ResultData<MercadoResponseDto>> DeleteAsync(int id);
    }
}
