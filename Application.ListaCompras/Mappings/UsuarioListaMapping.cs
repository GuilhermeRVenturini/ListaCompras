using Application.ListaCompras.DTOs.UsuarioLista;
using Domain.ListaCompras.Entities;

namespace Application.ListaCompras.Mappings
{
    internal static class UsuarioListaMapping
    {
        public static UsuarioLista ToEntity(int listaId, Guid usuarioId)
        {
            return new UsuarioLista
            {
                ListaId = listaId,
                UsuarioId = usuarioId
            };
        }

        public static UsuarioListaResponseDto ToResponseDto(UsuarioLista entity)
        {
            return new UsuarioListaResponseDto
            {
                ListaId = entity.ListaId,
                ListaNome = entity.Lista?.Nome ?? string.Empty,
                UsuarioId = entity.UsuarioId,
                UsuarioNome = entity.Usuario?.Nome ?? string.Empty,
                UsuarioEmail = entity.Usuario?.Email ?? string.Empty
            };
        }
    }
}
