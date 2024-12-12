namespace ReplaceHelper.Interfaces;

public record ReplaceHelperOptions(
    [StringSyntax(nameof(Regex))]string SplitPattern = @"\b\w+\b",
    StringComparison ComparisonType = StringComparison.Ordinal,
    string specSymbolsForTemplate = "{}");