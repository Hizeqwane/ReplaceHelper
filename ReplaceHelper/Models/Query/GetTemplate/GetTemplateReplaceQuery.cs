namespace ReplaceHelper.Models.Query.GetTemplate;

/// <summary>
/// Запрос на построение шаблона
/// </summary>
public class GetTemplateReplaceQuery
{
    public List<GetTemplateReplacementQuery> Replacements { get; } = [];
}