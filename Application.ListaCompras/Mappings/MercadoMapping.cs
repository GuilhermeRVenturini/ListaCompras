using Application.ListaCompras.DTOs.Mercado;
using Domain.ListaCompras.Entities;

namespace Application.ListaCompras.Mappings
{
    internal static class MercadoMapping
    {
        public static Mercado ToEntity(MercadoRequestDto request)
        {
            return new Mercado
            {
                Nome = request.Nome,
                HorarioFuncionamento = request.HorarioFuncionamento,
                Endereco = request.Endereco
            };
        }

        public static void MapToEntity(MercadoRequestDto request, Mercado entity)
        {
            entity.Nome = request.Nome;
            entity.HorarioFuncionamento = request.HorarioFuncionamento;
            entity.Endereco = request.Endereco;
        }

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
