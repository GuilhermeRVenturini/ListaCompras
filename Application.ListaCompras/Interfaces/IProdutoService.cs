using Application.ListaCompras.DTOs.Produto;
using Domain.ListaCompras.Common.Results;

namespace Application.ListaCompras.Interfaces
{
    public interface IProdutoService
    {
        Task<ResultData<IEnumerable<ProdutoResponseDto>>> GetAllAsync();

        Task<ResultData<ProdutoResponseDto>> GetByIdAsync(int id);

        Task<ResultData<ProdutoResponseDto>> CreateAsync(ProdutoRequestDto request);

        Task<ResultData<ProdutoResponseDto>> UpdateAsync(int id, ProdutoRequestDto request);

        Task<ResultData<ProdutoResponseDto>> DeleteAsync(int id);
    }
}
