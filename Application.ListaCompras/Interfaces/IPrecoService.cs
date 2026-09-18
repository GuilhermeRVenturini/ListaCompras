using Application.ListaCompras.DTOs.Preco;
using Domain.ListaCompras.Common.Results;

namespace Application.ListaCompras.Interfaces
{
    public interface IPrecoService
    {
        Task<ResultData<IEnumerable<PrecoResponseDto>>> GetAllAsync();

        Task<ResultData<PrecoResponseDto>> GetByIdAsync(int id);

        Task<ResultData<PrecoResponseDto>> CreateAsync(PrecoRequestDto request);

        Task<ResultData<PrecoResponseDto>> UpdateAsync(int id, PrecoRequestDto request);

        Task<ResultData<PrecoResponseDto>> DeleteAsync(int id);
    }
}
