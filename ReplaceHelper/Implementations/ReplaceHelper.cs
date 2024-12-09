namespace ReplaceHelper.Implementations;

/// <inheritdoc />
public class ReplaceHelper(
    ReplaceHelperOptions replaceHelperOptions)
    : IReplaceHelper
{
    /// <inheritdoc />
    public ReplaceHarvest GetReplaceHarvest(string input1, string input2)
    {
        var list1 = Split(input1);
        var list2 = Split(input2);
        
        var minLength = Math.Min(list1.Length, list2.Length);

        var replaceHarvest = new ReplaceHarvest();
        
        for (var i = 0; i < minLength; i++)
        {
            if (list1[i].Equals(list2[i], replaceHelperOptions.ComparisonType)) 
                continue;
            
            var foundedReplacement = replaceHarvest.Replacements
                .FirstOrDefault(s => s.FirstStr.Equals(list1[i], replaceHelperOptions.ComparisonType) &&
                                     s.SecondStr.Equals(list2[i], replaceHelperOptions.ComparisonType));
            if (foundedReplacement == null)
            {
                var position = new ReplacePosition
                (
                    input1.IndexOf(list1[i], replaceHelperOptions.ComparisonType),
                    input2.IndexOf(list2[i], replaceHelperOptions.ComparisonType)
                );
                
                var replacement = new Replacement(list1[i], list2[i], position);
                
                replaceHarvest.Replacements.Add(replacement);    
            }
            else
            {
                var position = new ReplacePosition
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
    public string GetReplaceTemplate(string input1, string input2, ReplaceTemplate template)
    {
        throw new NotImplementedException();
    }

    private string[] Split(string input) =>
        Regex.Matches(input, replaceHelperOptions.SplitPattern)
            .Select(m => m.Value)
            .ToArray();
}