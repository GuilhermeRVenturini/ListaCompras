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

        public static void MapToExistingEntity(PrecoPatchDto request, Preco entity)
        {
            if (request.Valor is not null)
                entity.Valor = request.Valor.Value;
            if (request.Desconto is not null)
                entity.Desconto = request.Desconto.Value;
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
