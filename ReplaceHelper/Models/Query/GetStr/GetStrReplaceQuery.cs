namespace ReplaceHelper.Models.Query.GetStr;

/// <summary>
/// Запрос на выполнение замен в шаблоне
/// </summary>
public class GetStrReplaceQuery(IEnumerable<GetStrReplacementQuery> replacements)
{
    public List<GetStrReplacementQuery> Replacements { get; } = replacements.ToList();
}