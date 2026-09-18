using Application.ListaCompras.DTOs.Usuario;
using Domain.ListaCompras.Entities;

namespace Application.ListaCompras.Mappings
{
    internal static class UsuarioMapping
    {
        public static Usuario ToEntity(UsuarioRequestDto request)
        {
            return new Usuario
            {
                Cpf = request.Cpf,
                Nome = request.Nome,
                Email = request.Email
            };
        }

        public static void MapToEntity(UsuarioRequestDto request, Usuario entity)
        {
            entity.Cpf = request.Cpf;
            entity.Nome = request.Nome;
            entity.Email = request.Email;
        }

        public static UsuarioResponseDto ToResponseDto(Usuario entity)
        {
            return new UsuarioResponseDto
            {
                Id = entity.Id,
                Cpf = entity.Cpf,
                Nome = entity.Nome,
                Email = entity.Email
            };
        }
    }
}
