namespace ReplaceHelper.Models;

/// <summary>
/// Заготовка замен
/// </summary>
public class ReplaceHarvest
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();

    /// <summary>
    /// Замены
    /// </summary>
    public List<Replacement> Replacements { get; } = [];
}