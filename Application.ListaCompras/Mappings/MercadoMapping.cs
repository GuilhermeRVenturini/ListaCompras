using Application.ListaCompras.DTOs.Mercado;
using Domain.ListaCompras.Entities;

// aqui faço a conversão de Mercado entre entidade e DTO
namespace Application.ListaCompras.Mappings
{

    internal static class MercadoMapping
    {

        // pega o DTO e monta uma entidade pra usar internamente
        public static Mercado ToEntity(MercadoRequestDto request)
        {
            return new Mercado
            {
                Nome = request.Nome,
                HorarioFuncionamento = request.HorarioFuncionamento,
                Endereco = request.Endereco
            };
        }

        // troca os dados na entidade que já existe, sem criar outra
        public static void MapToExistingEntity(MercadoRequestDto request, Mercado entity)
        {
            entity.Nome = request.Nome;
            entity.HorarioFuncionamento = request.HorarioFuncionamento;
            entity.Endereco = request.Endereco;
        }

        // monta o DTO que vai voltar na resposta
        public static MercadoResponseDto ToResponseDto(Mercado entity)
        {
            return new MercadoResponseDto
            {
                Id = entity.Id,
                Nome = entity.Nome,
                HorarioFuncionamento = entity.HorarioFuncionamento,
                Endereco = entity.Endereco
            };
        }
    }
}
