using Application.ListaCompras.DTOs.ProdutoLista;
using Domain.ListaCompras.Common.Results;

namespace Application.ListaCompras.Interfaces
{
    public interface IProdutoListaService
    {
        Task<ResultData<IEnumerable<ProdutoListaResponseDto>>> GetAllAsync();

        Task<ResultData<ProdutoListaResponseDto>> GetByIdAsync(int produtoId, int listaId);

        Task<ResultData<ProdutoListaResponseDto>> CreateAsync(ProdutoListaRequestDto request);

        Task<ResultData<ProdutoListaResponseDto>> UpdateAsync(int produtoId, int listaId, ProdutoListaRequestDto request);

        Task<ResultData<ProdutoListaResponseDto>> DeleteAsync(int produtoId, int listaId);
    }
}
