namespace Application.ListaCompras.Validation;

internal static class PatchValidator
{
    /// <summary>
    /// Verifica se uma atualização parcial contém dados aproveitáveis.
    /// <list type="number">
    /// <item>Rejeita uma requisição nula.</item>
    /// <item>Exige ao menos uma propriedade com valor não nulo.</item>
    /// <item>Rejeita campos de texto preenchidos apenas com espaços.</item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// Como as propriedades opcionais usam <c>null</c>, não distingue um campo
    /// omitido de outro enviado explicitamente com valor nulo.
    /// </remarks>
    /// <returns>Mensagem de validação ou <c>null</c> quando não encontra erro.</returns>
    internal static string? Validate<T>(T request) where T : class
    {
        if (request is null)
            return "Informe os dados que deseja atualizar.";

        var propriedades = typeof(T).GetProperties();
        var valores = propriedades.Select(p => p.GetValue(request)).ToArray();

        if (valores.All(v => v is null))
            return "Informe pelo menos um campo para atualizar.";

        if (valores.OfType<string>().Any(string.IsNullOrWhiteSpace))
            return "Os campos de texto informados não podem estar vazios.";

        return null;
    }
}
