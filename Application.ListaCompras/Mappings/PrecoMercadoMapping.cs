using Application.ListaCompras.DTOs.PrecoMercado;
using Domain.ListaCompras.Entities;

// aqui faço a conversão de PrecoMercado entre entidade e DTO
namespace Application.ListaCompras.Mappings
{

    internal static class PrecoMercadoMapping
    {

        // pega o DTO e monta uma entidade pra usar internamente
        public static PrecoMercado ToEntity(int produtoId, int mercadoId, PrecoMercadoRequestDto request)
        {
            return new PrecoMercado
            {
                ProdutoId = produtoId,
                MercadoId = mercadoId,
                PrecoId = request.PrecoId
            };
        }

        // troca os dados na entidade que já existe, sem criar outra
        public static void MapToExistingEntity(PrecoMercadoRequestDto request, PrecoMercado entity)
        {
            entity.PrecoId = request.PrecoId;
        }

        // monta o DTO que vai voltar na resposta
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
