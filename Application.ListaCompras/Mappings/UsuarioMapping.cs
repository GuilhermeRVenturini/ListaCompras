using Application.ListaCompras.DTOs.Usuario;
using Domain.ListaCompras.Entities;

// aqui faço a conversão de Usuario entre entidade e DTO
namespace Application.ListaCompras.Mappings
{

    internal static class UsuarioMapping
    {

        // pega o DTO e monta uma entidade pra usar internamente
        public static Usuario ToEntity(UsuarioRequestDto request)
        {
            return new Usuario
            {
                Cpf = request.Cpf,
                Nome = request.Nome,
                Email = request.Email
            };
        }

        // troca os dados na entidade que já existe, sem criar outra
        public static void MapToExistingEntity(UsuarioRequestDto request, Usuario entity)
        {
            entity.Cpf = request.Cpf;
            entity.Nome = request.Nome;
            entity.Email = request.Email;
        }

        // monta o DTO que vai voltar na resposta
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
