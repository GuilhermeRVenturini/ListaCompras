using System.Linq.Expressions;
using Domain.ListaCompras.Entities;

namespace Application.ListaCompras.Filters;

public sealed class UsuarioFilter
{
    public string? Nome { get; init; }
    public string? Cpf { get; init; }

    /// <summary>
    /// Monta a expressão de busca aplicada à consulta de usuários no banco.
    /// <list type="number">
    /// <item>Começa com <c>usuario =&gt; true</c>, permitindo uma busca sem restrições.</item>
    /// <item>Se houver nome, compara trechos do texto sem diferenciar maiúsculas de minúsculas.</item>
    /// <item>Se houver CPF, retira a pontuação e pesquisa pelos dígitos informados.</item>
    /// </list>
    /// Os critérios são combinados com <c>AND</c>. Um CPF sem dígitos não encontra usuários.
    /// </summary>
    /// <returns>Predicado LINQ que pode ser usado em uma consulta do Entity Framework.</returns>
    public Expression<Func<Usuario, bool>> ToExpression()
    {
        Expression<Func<Usuario, bool>> filtro = usuario => true;

        if (!string.IsNullOrWhiteSpace(Nome))
        {
            var nome = Nome.Trim().ToLowerInvariant();
            filtro = filtro.And(usuario => usuario.Nome.ToLower().Contains(nome));
        }

        if (!string.IsNullOrWhiteSpace(Cpf))
        {
            var cpf = new string(Cpf.Where(char.IsDigit).ToArray());
            if (cpf.Length > 0)
                filtro = filtro.And(usuario => usuario.Cpf.Contains(cpf));
            else
                filtro = filtro.And(usuario => false);
        }

        return filtro;
    }
}
