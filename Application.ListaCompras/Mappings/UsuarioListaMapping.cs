using Application.ListaCompras.DTOs.UsuarioLista;
using Domain.ListaCompras.Entities;

// aqui faço a conversão de UsuarioLista entre entidade e DTO
namespace Application.ListaCompras.Mappings
{

    internal static class UsuarioListaMapping
    {

        // pega o DTO e monta uma entidade pra usar internamente
        public static UsuarioLista ToEntity(int listaId, Guid usuarioId)
        {
            return new UsuarioLista
            {
                ListaId = listaId,
                UsuarioId = usuarioId
            };
        }

        // monta o DTO que vai voltar na resposta
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
