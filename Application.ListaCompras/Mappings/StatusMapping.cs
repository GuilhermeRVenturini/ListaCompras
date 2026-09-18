using Application.ListaCompras.DTOs.Status;
using Domain.ListaCompras.Entities;

namespace Application.ListaCompras.Mappings
{
    internal static class StatusMapping
    {
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
