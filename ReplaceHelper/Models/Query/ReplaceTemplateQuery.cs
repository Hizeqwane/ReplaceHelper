namespace ReplaceHelper.Models.Query;

/// <summary>
/// Запрос на построение шаблона
/// </summary>
public class ReplaceTemplateQuery
{
    public List<ReplacementQuery> Replacements { get; } = [];
}