using Application.ListaCompras.DTOs.ProdutoLista;
using Domain.ListaCompras.Entities;

namespace Application.ListaCompras.Mappings
{
    internal static class ProdutoListaMapping
    {
        public static ProdutoLista ToEntity(int produtoId, int listaId, ProdutoListaRequestDto request)
        {
            return new ProdutoLista
            {
                ProdutoId = produtoId,
                ListaId = listaId,
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
                ProdutoNome = entity.Produto?.Nome ?? string.Empty,
                ListaId = entity.ListaId,
                ListaNome = entity.Lista?.Nome ?? string.Empty,
                StatusId = entity.StatusId,
                StatusNome = entity.Status?.Nome ?? string.Empty,
                QuantidadeEstoque = entity.QuantidadeEstoque
            };
        }
    }
}
