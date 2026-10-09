using Application.ListaCompras.DTOs.Produto;
using Domain.ListaCompras.Entities;

// aqui faço a conversão de Produto entre entidade e DTO
namespace Application.ListaCompras.Mappings
{

    internal static class ProdutoMapping
    {

        // pega o DTO e monta uma entidade pra usar internamente
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

        // troca os dados na entidade que já existe, sem criar outra
        public static void MapToExistingEntity(ProdutoRequestDto request, Produto entity)
        {
            entity.Nome = request.Nome;
            entity.Descricao = request.Descricao;
            entity.Fabricante = request.Fabricante;
            entity.Validade = request.Validade;
        }

        // monta o DTO que vai voltar na resposta
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
