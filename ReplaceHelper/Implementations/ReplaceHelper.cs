using System.Text;
using ReplaceHelper.Models.Query.GetStr;
using ReplaceHelper.Models.Query.GetTemplate;
using ReplaceHelper.Models.Response;
using ReplaceHelper.Options;

namespace ReplaceHelper.Implementations;

/// <inheritdoc />
public class ReplaceHelper(
    ReplaceHelperOptions replaceHelperOptions)
    : IReplaceHelper
{
    /// <inheritdoc />
    public ReplaceHarvestResponse GetReplaceHarvest(string input1, string input2)
    {
        var wordIndexList1 = SplitWithIndex(input1);
        var wordIndexList2 = SplitWithIndex(input2);

        var minLength = Math.Min(wordIndexList1.Count, wordIndexList2.Count);

        var replaceHarvest = new ReplaceHarvestResponse();

        for (var i = 0; i < minLength; i++)
        {
            if (wordIndexList1[i].Word.Equals(wordIndexList2[i].Word, replaceHelperOptions.ComparisonType))
                continue;

            var foundedReplacement = replaceHarvest.Replacements
                // ReSharper disable AccessToModifiedClosure
                .FirstOrDefault
                (
                    s => s.IsStrings
                    (
                        wordIndexList1[i].Word,
                        wordIndexList2[i].Word,
                        replaceHelperOptions.ComparisonType
                    )
                );
            // ReSharper restore AccessToModifiedClosure

            var position = new ReplacePositionResponse
            (
                wordIndexList1[i].Index,
                wordIndexList2[i].Index
            );

            if (foundedReplacement == null)
            {
                var replacement = new ReplacementResponse(wordIndexList1[i].Word, wordIndexList2[i].Word, position);

                replaceHarvest.Replacements.Add(replacement);
            }
            else
            {
                foundedReplacement.Positions.Add(position);
            }
        }

        return replaceHarvest;
    }

    /// <inheritdoc />
    public string GetReplaceTemplate(string input1, GetTemplateReplaceQuery query)
    {
        var result = new StringBuilder(input1);

        var emptyPositionReplacements = query.Replacements
            .Where(s => s.Positions.Count == 0)
            .ToList();

        var replacementsByDescPositions = query.Replacements
            .SelectMany(s =>
                s.Positions.Select(p => (s.Str, p.Index, Positions: s.Positions.OrderBy(pos => pos.Index).ToList())))
            .OrderByDescending(s => s.Index);

        foreach (var replacement in replacementsByDescPositions)
        {
            if (replacement.Index > result.Length - 1)
                throw new ArgumentException(
                    $"Индекс замены {replacement.Index} для подстроки {replacement.Str} больше длины строки.");

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

    /// <inheritdoc />
    public string GetStrByTemplate(string template, GetStrReplaceQuery replaceQuery)
    {
        var result = new StringBuilder(template);

        foreach (var replacement in replaceQuery.Replacements)
            result.Replace(GetSpecSymbolStr(replacement.TemplateValue), replacement.NewValue);

        return result.ToString();
    }

    private string GetSpecSymbolStr(string str) =>
        $"{replaceHelperOptions.SpecSymbolsForTemplate[..(replaceHelperOptions.SpecSymbolsForTemplate.Length / 2)]}" +
        $"{str}" +
        $"{replaceHelperOptions.SpecSymbolsForTemplate[(replaceHelperOptions.SpecSymbolsForTemplate.Length / 2)..]}";

    private List<(string Word, int Index)> SplitWithIndex(string input)
    {
        var result = new List<(string, int)>();

        Func<char, bool> isCharFunc = ch =>
            replaceHelperOptions.WordSymbols.Contains(ch);

        if ((replaceHelperOptions.SymbolOptions & WordSymbol.Letter) != 0)
        {
            var previousIsCharFunc = isCharFunc;
            isCharFunc = ch =>
                previousIsCharFunc(ch) || char.IsLetter(ch);
        }

        if ((replaceHelperOptions.SymbolOptions & WordSymbol.Number) != 0)
        {
            var previousIsCharFunc = isCharFunc;
            isCharFunc = ch =>
                previousIsCharFunc(ch) || char.IsNumber(ch);
        }

        if ((replaceHelperOptions.SymbolOptions & WordSymbol.Punctuation) != 0)
        {
            var previousIsCharFunc = isCharFunc;
            isCharFunc = ch =>
                previousIsCharFunc(ch) || char.IsPunctuation(ch);
        }

        var currentWord = new StringBuilder();
        var currentWordStartInd = 0;

        for (var i = 0; i < input.Length; i++)
        {
            if (isCharFunc(input[i]))
            {
                if (currentWord.Length == 0)
                    currentWordStartInd = i;

                currentWord.Append(input[i]);
                continue;
            }

            if (currentWord.Length != 0)
            {
                result.Add((currentWord.ToString(), currentWordStartInd));
            }

            currentWord.Clear();
        }

        if (currentWord.Length != 0)
        {
            result.Add((currentWord.ToString(), currentWordStartInd));
        }


        return result;
    }
}