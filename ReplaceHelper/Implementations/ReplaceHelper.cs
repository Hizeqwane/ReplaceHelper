using System.Text.RegularExpressions;
using ReplaceHelper.Interfaces;
using ReplaceHelper.Models;

namespace ReplaceHelper.Implementations;

/// <inheritdoc />
internal class ReplaceHelper : IReplaceHelper
{
    /// <inheritdoc />
    public ReplaceTemplate GetReplaceTemplate(string input1, string input2)
    {
        throw new NotImplementedException();
    }
    
    static string[] SplitByNonAlphanumeric(string input)
    {
        // Регулярное выражение для разделения по всем символам, которые не являются буквами или цифрами
        var pattern = @"[^a-zA-Z0-9]+";
        
        return Regex.Split(input, pattern);
    }
}