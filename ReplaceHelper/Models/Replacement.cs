namespace ReplaceHelper.Models;

/// <summary>
/// Замена
/// </summary>
public class Replacement
{
    public Replacement(
        string firstStr,
        string secondStr,
        ReplacePosition replacePosition)
    {
        FirstStr = firstStr;
        SecondStr = secondStr;
        Positions.Add(replacePosition);
    }
    
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();

    /// <summary>
    /// Подстрока в первом объекте
    /// </summary>
    public string FirstStr { get; }
    
    /// <summary>
    /// Подстрока во втором объекте
    /// </summary>
    public string SecondStr { get; }
    
    /// <summary>
    /// Сопоставление позиций в первом и втором объектах
    /// </summary>
    public List<ReplacePosition> Positions { get; } = [];

    public override int GetHashCode() => 
        (FirstStr + SecondStr).GetHashCode();

    public bool Equals(Replacement replacement, StringComparison stringComparison) =>
        replacement.FirstStr.Equals(FirstStr, stringComparison) &&
        replacement.SecondStr.Equals(SecondStr, stringComparison);
}