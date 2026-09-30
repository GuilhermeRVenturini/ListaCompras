using Application.ListaCompras.DTOs.Historico;
using Domain.ListaCompras.Entities;

namespace Application.ListaCompras.Mappings
{
    internal static class HistoricoMapping
    {
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
