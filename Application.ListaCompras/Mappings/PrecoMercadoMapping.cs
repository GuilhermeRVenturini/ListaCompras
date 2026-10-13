using Application.ListaCompras.DTOs.PrecoMercado;
using Domain.ListaCompras.Entities;

namespace Application.ListaCompras.Mappings
{

    internal static class PrecoMercadoMapping
    {

        public static PrecoMercado ToEntity(int produtoId, int mercadoId, PrecoMercadoRequestDto request)
        {
            return new PrecoMercado
            {
                ProdutoId = produtoId,
                MercadoId = mercadoId,
                PrecoId = request.PrecoId
            };
        }

        public static void MapToExistingEntity(PrecoMercadoPatchDto request, PrecoMercado entity)
        {
            if (request.PrecoId is not null)
                entity.PrecoId = request.PrecoId.Value;
        }

        public static PrecoMercadoResponseDto ToResponseDto(PrecoMercado entity)
        {
            return new PrecoMercadoResponseDto
            {
                ProdutoId = entity.ProdutoId,
                ProdutoNome = entity.Produto?.Nome ?? string.Empty,
                MercadoId = entity.MercadoId,
                MercadoNome = entity.Mercado?.Nome ?? string.Empty,
                PrecoId = entity.PrecoId,
                Valor = entity.Preco?.Valor ?? 0,
                Desconto = entity.Preco?.Desconto ?? false
            };
        }
    }
}
