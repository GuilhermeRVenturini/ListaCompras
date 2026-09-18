using Application.ListaCompras.DTOs.UsuarioLista;
using Domain.ListaCompras.Entities;

namespace Application.ListaCompras.Mappings
{
    internal static class UsuarioListaMapping
    {
        public static UsuarioLista ToEntity(UsuarioListaRequestDto request)
        {
            return new UsuarioLista
            {
                ListaId = request.ListaId,
                UsuarioId = request.UsuarioId
            };
        }

        public static UsuarioListaResponseDto ToResponseDto(UsuarioLista entity)
        {
            return new UsuarioListaResponseDto
            {
                ListaId = entity.ListaId,
                UsuarioId = entity.UsuarioId
            };
        }
    }
}
