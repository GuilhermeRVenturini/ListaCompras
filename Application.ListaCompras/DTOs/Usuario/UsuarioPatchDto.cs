using System.ComponentModel.DataAnnotations;

namespace Application.ListaCompras.DTOs.Usuario;

public sealed class UsuarioPatchDto
{
    [MaxLength(11)]
    public string? Cpf { get; init; }

    [MaxLength(64)]
    public string? Nome { get; init; }

    [MaxLength(64)]
    public string? Email { get; init; }

}
