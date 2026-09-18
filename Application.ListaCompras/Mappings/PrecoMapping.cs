using Application.ListaCompras.DTOs.Preco;
using Domain.ListaCompras.Entities;

namespace Application.ListaCompras.Mappings
{
    internal static class PrecoMapping
    {
        public static Preco ToEntity(PrecoRequestDto request)
        {
            return new Preco
            {
                Valor = request.Valor,
                Desconto = request.Desconto
            };
        }

        public static void MapToEntity(PrecoRequestDto request, Preco entity)
        {
            entity.Valor = request.Valor;
            entity.Desconto = request.Desconto;
        }

        public static PrecoResponseDto ToResponseDto(Preco entity)
        {
            return new PrecoResponseDto
            {
                Id = entity.Id,
                Valor = entity.Valor,
                Desconto = entity.Desconto
            };
        }
    }
}
