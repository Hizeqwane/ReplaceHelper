namespace ReplaceHelper.Models.Response;

/// <summary>
/// Заготовка замен
/// </summary>
public class ReplaceHarvestResponse
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();

    /// <summary>
    /// Замены
    /// </summary>
    public List<ReplacementResponse> Replacements { get; } = [];
}