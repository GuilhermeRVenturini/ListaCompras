using Application.ListaCompras.DTOs.Lista;
using Domain.ListaCompras.Entities;

namespace Application.ListaCompras.Mappings
{

    internal static class ListaMapping
    {

        public static Lista ToEntity(ListaRequestDto request)
        {
            return new Lista
            {
                DataListaCriacao = request.DataListaCriacao,
                Nome = request.Nome
            };
        }

        public static void MapToExistingEntity(ListaPatchDto request, Lista entity)
        {
            if (request.DataListaCriacao is not null)
                entity.DataListaCriacao = request.DataListaCriacao.Value;
            if (request.Nome is not null)
                entity.Nome = request.Nome;
        }

        public static ListaResponseDto ToResponseDto(Lista entity)
        {
            return new ListaResponseDto
            {
                Id = entity.Id,
                DataListaCriacao = entity.DataListaCriacao,
                Nome = entity.Nome
            };
        }
    }
}
