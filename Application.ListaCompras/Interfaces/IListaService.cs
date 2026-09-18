using Application.ListaCompras.DTOs.Lista;
using Domain.ListaCompras.Common.Results;

namespace Application.ListaCompras.Interfaces
{
    public interface IListaService
    {
        Task<ResultData<IEnumerable<ListaResponseDto>>> GetAllAsync();

        Task<ResultData<ListaResponseDto>> GetByIdAsync(int id);

        Task<ResultData<ListaResponseDto>> CreateAsync(ListaRequestDto request);

        Task<ResultData<ListaResponseDto>> UpdateAsync(int id, ListaRequestDto request);

        Task<ResultData<ListaResponseDto>> DeleteAsync(int id);
    }
}
