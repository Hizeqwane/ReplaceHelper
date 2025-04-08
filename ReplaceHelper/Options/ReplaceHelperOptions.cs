namespace ReplaceHelper.Options;

public record ReplaceHelperOptions(
    WordSymbol SymbolOptions = WordSymbol.Letter | WordSymbol.Number,
    string WordSymbols = "",
    StringComparison ComparisonType = StringComparison.Ordinal,
    string SpecSymbolsForTemplate = "{}");