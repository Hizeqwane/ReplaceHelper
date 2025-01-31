using ReplaceHelper.Models.Query.GetStr;
using ReplaceHelper.Models.Query.GetTemplate;
using ReplaceHelper.Models.Response;

namespace ReplaceHelper.Interfaces;

/// <summary>
/// Сервис, помогающий при заменах
/// </summary>
public interface IReplaceHelper
{
    /// <summary>
    /// Получить заготовку для замен по двум экземплярам
    /// </summary>
    ReplaceHarvestResponse GetReplaceHarvest(string input1, string input2);
    
    /// <summary>
    /// Получить шаблон для замен
    /// </summary>
    string GetReplaceTemplate(string input1, GetTemplateReplaceQuery query);

    /// <summary>
    /// Получить экземпляр по шаблону и заменам
    /// </summary>
    string GetStrByTemplate(string template, GetStrReplaceQuery replaceQuery);
}