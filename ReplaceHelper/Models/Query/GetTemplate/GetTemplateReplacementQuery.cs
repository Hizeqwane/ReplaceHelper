namespace ReplaceHelper.Models.Query.GetTemplate;

/// <summary>
/// Замена
/// </summary>
public class GetTemplateReplacementQuery(
    string str,
    IEnumerable<int> positions)
{
    /// <summary>
    /// Подстрока, которая будет заменена
    /// </summary>
    public string Str { get; } = str;

    /// <summary>
    /// Позиции
    /// </summary>
    public List<GetTemplateReplacePositionQuery> Positions { get; } = positions
        .Select(s => new GetTemplateReplacePositionQuery(s))
        .ToList();
}