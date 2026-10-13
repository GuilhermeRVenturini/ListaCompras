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

        /// <summary>
        /// Copia para o usuário carregado apenas os campos informados no PATCH.
        /// A entidade existente é reaproveitada para preservar os demais valores
        /// e o rastreamento de alterações do Entity Framework.
        /// </summary>
        public static void MapToExistingEntity(UsuarioPatchDto request, Usuario entity)
        {
            if (request.Cpf is not null)
                entity.Cpf = request.Cpf;
            if (request.Nome is not null)
                entity.Nome = request.Nome;
            if (request.Email is not null)
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
