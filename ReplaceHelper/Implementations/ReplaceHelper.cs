using System.Text;
using ReplaceHelper.Models.Query;
using ReplaceHelper.Models.Response;

namespace ReplaceHelper.Implementations;

/// <inheritdoc />
public class ReplaceHelper(
    ReplaceHelperOptions replaceHelperOptions)
    : IReplaceHelper
{
    /// <inheritdoc />
    public ReplaceHarvestResponse GetReplaceHarvest(string input1, string input2)
    {
        var list1 = Split(input1);
        var list2 = Split(input2);
        
        var minLength = Math.Min(list1.Length, list2.Length);

        var replaceHarvest = new ReplaceHarvestResponse();
        
        for (var i = 0; i < minLength; i++)
        {
            if (list1[i].Equals(list2[i], replaceHelperOptions.ComparisonType)) 
                continue;
            
            var foundedReplacement = replaceHarvest.Replacements
                // ReSharper disable AccessToModifiedClosure
                .FirstOrDefault(s => s.IsStrings(list1[i], list2[i], replaceHelperOptions.ComparisonType));
                // ReSharper restore AccessToModifiedClosure
            if (foundedReplacement == null)
            {
                var position = new ReplacePositionResponse
                (
                    input1.IndexOf(list1[i], replaceHelperOptions.ComparisonType),
                    input2.IndexOf(list2[i], replaceHelperOptions.ComparisonType)
                );
                
                var replacement = new ReplacementResponse(list1[i], list2[i], position);
                
                replaceHarvest.Replacements.Add(replacement);    
            }
            else
            {
                var position = new ReplacePositionResponse
                (
                    input1[(foundedReplacement.Positions.Last().FirstIndex + 1)..].IndexOf(list1[i],
                        replaceHelperOptions.ComparisonType),
                    input2[(foundedReplacement.Positions.Last().SecondIndex + 1)..].IndexOf(list2[i],
                        replaceHelperOptions.ComparisonType)
                );
                
                foundedReplacement.Positions.Add(position);
            }
        }

        return replaceHarvest;
    }

    /// <inheritdoc />
    public string GetReplaceTemplate(string input1, ReplaceTemplateQuery templateQuery)
    {
        var result = new StringBuilder(input1);

        var emptyPositionReplacements = templateQuery.Replacements
            .Where(s => s.Positions.Count == 0)
            .ToList();
        
        var replacementsByDescPositions = templateQuery.Replacements
            .SelectMany(s => s.Positions.Select(p => (s.Str, p.Index, Positions: s.Positions.OrderBy(pos => pos.Index).ToList())))
            .OrderByDescending(s => s.Index);
        
        foreach (var replacement in replacementsByDescPositions)
        {
            if (replacement.Index > result.Length - 1)
                throw new ArgumentException($"Индекс замены {replacement.Index} для подстроки {replacement.Str} больше длины строки.");

            result.Replace
            (
                replacement.Str,
                GetSpecSymbolStr($"{replacement.Str}{string.Join('_', replacement.Positions.Select(s => s.Index))}"),
                replacement.Index,
                replacement.Str.Length
            );
        }

        foreach (var emptyPositionReplacement in emptyPositionReplacements)
            result.Replace
            (
                emptyPositionReplacement.Str,
                GetSpecSymbolStr($"{emptyPositionReplacement.Str}")
            );

        return result.ToString();
    }

    private string GetSpecSymbolStr(string str) =>
        $"{replaceHelperOptions.specSymbolsForTemplate[..(replaceHelperOptions.specSymbolsForTemplate.Length / 2)]}" +
        $"{str}" +
        $"{replaceHelperOptions.specSymbolsForTemplate[(replaceHelperOptions.specSymbolsForTemplate.Length / 2)..]}";
    
    private string[] Split(string input) =>
        Regex.Matches(input, replaceHelperOptions.SplitPattern)
            .Select(m => m.Value)
            .ToArray();
}