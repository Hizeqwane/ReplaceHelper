namespace ReplaceHelper.Interfaces;

/// <summary>
/// Сервис, помогающий при заменах
/// </summary>
public interface IReplaceHelper
{
    /// <summary>
    /// Получить заготовку для замен по двум экземплярам
    /// </summary>
    ReplaceHarvest GetReplaceHarvest(string input1, string input2);
    
    /// <summary>
    /// Получить шаблон для замен по двум экземплярам и заготовке
    /// </summary>
    string GetReplaceTemplate(string input1, string input2, ReplaceTemplate template);
}