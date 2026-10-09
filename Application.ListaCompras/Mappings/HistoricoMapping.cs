using Application.ListaCompras.DTOs.Historico;
using Domain.ListaCompras.Entities;

// aqui faço a conversão de Historico entre entidade e DTO
namespace Application.ListaCompras.Mappings
{

    internal static class HistoricoMapping
    {

        // monta o DTO que vai voltar na resposta
        public static HistoricoResponseDto ToResponseDto(Historico entity)
        {
            return new HistoricoResponseDto
            {
                Id = entity.Id,
                Entidade = entity.Entidade,
                Operacao = entity.Operacao,
                Registro = entity.Registro,
                DataRegistro = entity.DataRegistro
            };
        }
    }
}
