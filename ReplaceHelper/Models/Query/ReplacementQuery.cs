namespace ReplaceHelper.Models.Query;

/// <summary>
/// Замена
/// </summary>
public class ReplacementQuery(
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
    public List<ReplacePositionQuery> Positions { get; } = positions
        .Select(s => new ReplacePositionQuery(s))
        .ToList();
}