using ReplaceHelper.Models;

namespace ReplaceHelper.Interfaces;

/// <summary>
/// Сервис, помогающий при заменах
/// </summary>
public interface IReplaceHelper
{
    /// <summary>
    /// Получить шаблон для замен по двум экземплярам
    /// </summary>
    ReplaceTemplate GetReplaceTemplate(string input1, string input2);
}