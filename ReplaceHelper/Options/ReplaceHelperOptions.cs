namespace ReplaceHelper.Options;

public record ReplaceHelperOptions(
    WordSymbol SymbolOptions = WordSymbol.Number | WordSymbol.Number,
    string WordSymbols = "",
    StringComparison ComparisonType = StringComparison.Ordinal,
    string SpecSymbolsForTemplate = "{}");