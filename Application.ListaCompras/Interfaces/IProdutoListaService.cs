using Application.ListaCompras.DTOs.ProdutoLista;
using Domain.ListaCompras.Common.Results;

namespace Application.ListaCompras.Interfaces
{
    public interface IProdutoListaService
    {
        Task<ResultData<IEnumerable<ProdutoListaResponseDto>>> GetAllAsync();
        Task<ResultData<IEnumerable<ProdutoListaResponseDto>>> GetByListaAsync(int listaId);
        Task<ResultData<IEnumerable<ProdutoListaResponseDto>>> GetByProdutoAsync(int produtoId);
        Task<ResultData<ProdutoListaResponseDto>> GetByIdAsync(int produtoId, int listaId);
        Task<ResultData<ProdutoListaResponseDto>> CreateAsync(int listaId, int produtoId, ProdutoListaRequestDto request);
        Task<ResultData<ProdutoListaResponseDto>> UpdateAsync(int listaId, int produtoId, ProdutoListaRequestDto request);
        Task<ResultData<ProdutoListaResponseDto>> DeleteAsync(int produtoId, int listaId);
    }
}
