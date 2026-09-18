using Application.ListaCompras.DTOs.PrecoMercado;
using Domain.ListaCompras.Entities;

namespace Application.ListaCompras.Mappings
{
    internal static class PrecoMercadoMapping
    {
        public static PrecoMercado ToEntity(PrecoMercadoRequestDto request)
        {
            return new PrecoMercado
            {
                ProdutoId = request.ProdutoId,
                MercadoId = request.MercadoId,
                PrecoId = request.PrecoId
            };
        }

        public static void MapToEntity(PrecoMercadoRequestDto request, PrecoMercado entity)
        {
            entity.PrecoId = request.PrecoId;
        }

        public static PrecoMercadoResponseDto ToResponseDto(PrecoMercado entity)
        {
            return new PrecoMercadoResponseDto
            {
                ProdutoId = entity.ProdutoId,
                MercadoId = entity.MercadoId,
                PrecoId = entity.PrecoId
            };
        }
    }
}
