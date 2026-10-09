using Application.ListaCompras.DTOs.ProdutoLista;
using Domain.ListaCompras.Entities;

// aqui faço a conversão de ProdutoLista entre entidade e DTO
namespace Application.ListaCompras.Mappings
{

    internal static class ProdutoListaMapping
    {

        // pega o DTO e monta uma entidade pra usar internamente
        public static ProdutoLista ToEntity(int produtoId, int listaId, ProdutoListaRequestDto request)
        {
            return new ProdutoLista
            {
                ProdutoId = produtoId,
                ListaId = listaId,
                StatusId = request.StatusId,
                QuantidadeEstoque = request.QuantidadeEstoque
            };
        }

        // troca os dados na entidade que já existe, sem criar outra
        public static void MapToExistingEntity(ProdutoListaRequestDto request, ProdutoLista entity)
        {
            entity.StatusId = request.StatusId;
            entity.QuantidadeEstoque = request.QuantidadeEstoque;
        }

        // monta o DTO que vai voltar na resposta
        public static ProdutoListaResponseDto ToResponseDto(ProdutoLista entity)
        {
            return new ProdutoListaResponseDto
            {
                ProdutoId = entity.ProdutoId,
                ProdutoNome = entity.Produto?.Nome ?? string.Empty,
                ListaId = entity.ListaId,
                ListaNome = entity.Lista?.Nome ?? string.Empty,
                StatusId = entity.StatusId,
                StatusNome = entity.Status?.Nome ?? string.Empty,
                QuantidadeEstoque = entity.QuantidadeEstoque
            };
        }
    }
}
