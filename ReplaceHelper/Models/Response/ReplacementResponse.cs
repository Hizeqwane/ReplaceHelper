namespace ReplaceHelper.Models.Response;

/// <summary>
/// Замена
/// </summary>
public class ReplacementResponse
{
    public ReplacementResponse(
        string firstStr,
        string secondStr,
        ReplacePositionResponse replacePositionResponse)
    {
        FirstStr = firstStr;
        SecondStr = secondStr;
        Positions.Add(replacePositionResponse);
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
    public List<ReplacePositionResponse> Positions { get; } = [];

    public bool IsStrings(
        string input1,
        string input2,
        StringComparison stringComparison = StringComparison.OrdinalIgnoreCase) =>
        FirstStr.Equals(input1, stringComparison) &&
        SecondStr.Equals(input2, stringComparison);
}