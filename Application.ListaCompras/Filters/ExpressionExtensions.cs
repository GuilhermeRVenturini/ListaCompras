using System.Linq.Expressions;

namespace Application.ListaCompras.Filters;

internal static class ExpressionExtensions
{
    /// <summary>
    /// Combina dois filtros usando <c>AND</c> sem executá-los em memória.
    /// Reutiliza o parâmetro da primeira expressão na segunda para formar
    /// uma única árvore de expressão que o Entity Framework pode traduzir.
    /// </summary>
    internal static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> esquerda, Expression<Func<T, bool>> direita)
    {
        var parametro = esquerda.Parameters[0];
        var corpoDireito = new ReplaceParameterVisitor(direita.Parameters[0], parametro)
            .Visit(direita.Body)!;

        return Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(esquerda.Body, corpoDireito), parametro);
    }

    private sealed class ReplaceParameterVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _origem;
        private readonly ParameterExpression _destino;

        internal ReplaceParameterVisitor(ParameterExpression origem, ParameterExpression destino)
        {
            _origem = origem;
            _destino = destino;
        }

        protected override Expression VisitParameter(ParameterExpression node)
            => node == _origem ? _destino : base.VisitParameter(node);
    }
}
