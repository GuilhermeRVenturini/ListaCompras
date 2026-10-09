using Application.ListaCompras.DTOs.Lista;
using Domain.ListaCompras.Entities;

// aqui faço a conversão de Lista entre entidade e DTO
namespace Application.ListaCompras.Mappings
{

    internal static class ListaMapping
    {

        // pega o DTO e monta uma entidade pra usar internamente
        public static Lista ToEntity(ListaRequestDto request)
        {
            return new Lista
            {
                DataListaCriacao = request.DataListaCriacao,
                Nome = request.Nome
            };
        }

        // troca os dados na entidade que já existe, sem criar outra
        public static void MapToExistingEntity(ListaRequestDto request, Lista entity)
        {
            entity.DataListaCriacao = request.DataListaCriacao;
            entity.Nome = request.Nome;
        }

        // monta o DTO que vai voltar na resposta
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
