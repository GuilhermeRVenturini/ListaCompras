using Application.ListaCompras.DTOs.Historico;
using Domain.ListaCompras.Entities;

namespace Application.ListaCompras.Mappings
{
    internal static class HistoricoMapping
    {
        public static Historico ToEntity(HistoricoRequestDto request)
        {
            return new Historico
            {
                Registro = request.Registro,
                ProdutoId = request.ProdutoId,
                MercadoId = request.MercadoId,
                PrecoId = request.PrecoId,
                DataRegistro = request.DataRegistro
            };
        }

        public static void MapToEntity(HistoricoRequestDto request, Historico entity)
        {
            entity.Registro = request.Registro;
            entity.ProdutoId = request.ProdutoId;
            entity.MercadoId = request.MercadoId;
            entity.PrecoId = request.PrecoId;
            entity.DataRegistro = request.DataRegistro;
        }

        public static HistoricoResponseDto ToResponseDto(Historico entity)
        {
            return new HistoricoResponseDto
            {
                Id = entity.Id,
                Registro = entity.Registro,
                ProdutoId = entity.ProdutoId,
                MercadoId = entity.MercadoId,
                PrecoId = entity.PrecoId,
                DataRegistro = entity.DataRegistro
            };
        }
    }
}
