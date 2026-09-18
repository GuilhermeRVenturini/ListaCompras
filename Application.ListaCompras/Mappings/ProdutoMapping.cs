using Application.ListaCompras.DTOs.Produto;
using Domain.ListaCompras.Entities;

namespace Application.ListaCompras.Mappings
{
    internal static class ProdutoMapping
    {
        public static Produto ToEntity(ProdutoRequestDto request)
        {
            return new Produto
            {
                Nome = request.Nome,
                Descricao = request.Descricao,
                Fabricante = request.Fabricante,
                Validade = request.Validade
            };
        }

        public static void MapToEntity(ProdutoRequestDto request, Produto entity)
        {
            entity.Nome = request.Nome;
            entity.Descricao = request.Descricao;
            entity.Fabricante = request.Fabricante;
            entity.Validade = request.Validade;
        }

        public static ProdutoResponseDto ToResponseDto(Produto entity)
        {
            return new ProdutoResponseDto
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Descricao = entity.Descricao,
                Fabricante = entity.Fabricante,
                Validade = entity.Validade
            };
        }
    }
}
