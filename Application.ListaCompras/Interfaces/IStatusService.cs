using Application.ListaCompras.DTOs.Status;
using Domain.ListaCompras.Common.Results;

namespace Application.ListaCompras.Interfaces
{
    public interface IStatusService
    {
        Task<ResultData<IEnumerable<StatusResponseDto>>> GetAllAsync();

        Task<ResultData<StatusResponseDto>> GetByIdAsync(int id);

        Task<ResultData<StatusResponseDto>> CreateAsync(StatusRequestDto request);

        Task<ResultData<StatusResponseDto>> UpdateAsync(int id, StatusRequestDto request);

        Task<ResultData<StatusResponseDto>> DeleteAsync(int id);
    }
}
