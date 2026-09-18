using Application.ListaCompras.DTOs.ProdutoLista;
using Domain.ListaCompras.Entities;

namespace Application.ListaCompras.Mappings
{
    internal static class ProdutoListaMapping
    {
        public static ProdutoLista ToEntity(ProdutoListaRequestDto request)
        {
            return new ProdutoLista
            {
                ProdutoId = request.ProdutoId,
                ListaId = request.ListaId,
                StatusId = request.StatusId,
                QuantidadeEstoque = request.QuantidadeEstoque
            };
        }

        public static void MapToEntity(ProdutoListaRequestDto request, ProdutoLista entity)
        {
            entity.StatusId = request.StatusId;
            entity.QuantidadeEstoque = request.QuantidadeEstoque;
        }

        public static ProdutoListaResponseDto ToResponseDto(ProdutoLista entity)
        {
            return new ProdutoListaResponseDto
            {
                ProdutoId = entity.ProdutoId,
                ListaId = entity.ListaId,
                StatusId = entity.StatusId,
                QuantidadeEstoque = entity.QuantidadeEstoque
            };
        }
    }
}
