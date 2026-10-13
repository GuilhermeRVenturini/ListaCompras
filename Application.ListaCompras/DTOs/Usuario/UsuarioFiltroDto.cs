using System.ComponentModel.DataAnnotations;

namespace Application.ListaCompras.DTOs.Usuario;

public sealed class UsuarioFiltroDto
{
    [MaxLength(64)]
    public string? Nome { get; init; }

    [MaxLength(14)]
    public string? Cpf { get; init; }
}
