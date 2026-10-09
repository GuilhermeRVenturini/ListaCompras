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

        public static void MapToExistingEntity(ProdutoPatchDto request, Produto entity)
        {
            if (request.Nome is not null)
                entity.Nome = request.Nome;
            if (request.Descricao is not null)
                entity.Descricao = request.Descricao;
            if (request.Fabricante is not null)
                entity.Fabricante = request.Fabricante;
            if (request.Validade is not null)
                entity.Validade = request.Validade.Value;
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
