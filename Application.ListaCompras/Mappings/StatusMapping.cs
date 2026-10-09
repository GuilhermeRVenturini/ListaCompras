using Application.ListaCompras.DTOs.Status;
using Domain.ListaCompras.Entities;

// aqui faço a conversão de Status entre entidade e DTO
namespace Application.ListaCompras.Mappings
{

    internal static class StatusMapping
    {

        // pega o DTO e monta uma entidade pra usar internamente
        public static Status ToEntity(StatusRequestDto request)
        {
            return new Status
            {
                Nome = request.Nome
            };
        }

        public static void MapToEntity(StatusRequestDto request, Status entity)
        {
            entity.Nome = request.Nome;
        }

        // monta o DTO que vai voltar na resposta
        public static StatusResponseDto ToResponseDto(Status entity)
        {
            return new StatusResponseDto
            {
                Id = entity.Id,
                Nome = entity.Nome
            };
        }
    }
}
