namespace ReplaceHelper.Models;

/// <summary>
/// Замена
/// </summary>
public class Replacement
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Подстрока в первом объекте
    /// </summary>
    public string FirstStr { get; set; }
    
    /// <summary>
    /// Подстрока во втором объекте
    /// </summary>
    public string SecondStr { get; set; }
    
    /// <summary>
    /// Сопоставление позиций в первом и втором объектах
    /// </summary>
    public Dictionary<int, int> Positions { get; set; }
}