namespace ReplaceHelper.Models;

/// <summary>
/// Шаблон замен
/// </summary>
public class ReplaceTemplate
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Замены
    /// </summary>
    public List<Replacement> Replacements { get; set; }
}