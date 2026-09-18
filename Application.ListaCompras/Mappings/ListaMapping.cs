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

        public static void MapToEntity(ListaRequestDto request, Lista entity)
        {
            entity.DataListaCriacao = request.DataListaCriacao;
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
