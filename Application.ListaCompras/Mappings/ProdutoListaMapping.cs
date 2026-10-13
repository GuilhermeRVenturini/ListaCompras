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

        public static void MapToExistingEntity(ProdutoListaPatchDto request, ProdutoLista entity)
        {
            if (request.StatusId is not null)
                entity.StatusId = request.StatusId.Value;
            if (request.QuantidadeEstoque is not null)
                entity.QuantidadeEstoque = request.QuantidadeEstoque.Value;
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
