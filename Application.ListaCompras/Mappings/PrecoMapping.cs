using Application.ListaCompras.DTOs.Preco;
using Domain.ListaCompras.Entities;

// aqui faço a conversão de Preco entre entidade e DTO
namespace Application.ListaCompras.Mappings
{

    internal static class PrecoMapping
    {

        // pega o DTO e monta uma entidade pra usar internamente
        public static Preco ToEntity(PrecoRequestDto request)
        {
            return new Preco
            {
                Valor = request.Valor,
                Desconto = request.Desconto
            };
        }

        // troca os dados na entidade que já existe, sem criar outra
        public static void MapToExistingEntity(PrecoRequestDto request, Preco entity)
        {
            entity.Valor = request.Valor;
            entity.Desconto = request.Desconto;
        }

        // monta o DTO que vai voltar na resposta
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
