using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using ReplaceHelper.Interfaces;
using ReplaceHelper.Models.Query;
using ReplaceHelper.Models.Query.GetStr;
using ReplaceHelper.Models.Query.GetTemplate;
using ReplaceHelper.Options;

namespace ReplaceHelperTests;

public class UnitTest1
{
    private string GetTest1Result() =>
        File.ReadAllText
        (
            Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location) ?? string.Empty,
                "Data",
                "Test1Result.json")
        );

    private const string Str1= """
               Съешь ещё этих мягких французских булок, да выпей чаю
               Съешь ещё этих мягких французских булок, да выпей чаю
               Съешь ещё этих мягких французских булок, да выпей чаю
               Съешь ещё этих мягких французских булок, да выпей чаю
               Съешь ещё этих мягких французских булок, да выпей чаю
               """;
    
    private const string Str2 = """
               Съешь больше этих мягких французских булок, да выпей чаю
               Съешь ещё этих мягких китайских булок, да выпей чаю
               Съешь ещё этих мягких французских булок, да выпей чаю
               Съешь меньше этих мягких французских круассанов, да выпей кофе
               Съешь ещё этих мягких российских булок, да выпей кофе
               """;
    
    private const string Test2ExpectedResult = """
               Съешь {ещё6} этих мягких французских булок, да выпей чаю
               Съешь ещё этих мягких {французских77} булок, да выпей чаю
               Съешь ещё этих мягких французских булок, да выпей чаю
               Съешь {ещё171} этих мягких французских {булок199}, да выпей {чаю215_270}
               Съешь ещё этих мягких {французских242} булок, да выпей {чаю215_270}
               """;

    private readonly ReplaceHelperOptions _options = new();
    
    [Fact]
    public void Test1()
    {
        var replaceHelper = new ReplaceHelper.Implementations.ReplaceHelper(_options);
        
        var result = replaceHelper.GetReplaceHarvest(Str1, Str2);

        var strResult = JsonConvert.SerializeObject(result, Formatting.Indented);

        var emptyGuidStrResult = Regex
            .Replace(strResult, "[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}", Guid.Empty.ToString());

        var test1ExpectedResult = GetTest1Result();
        
        Assert.True(emptyGuidStrResult == test1ExpectedResult);
    }
    
    [Fact]
    public void Test2()
    {
        var replaceHelper = new ReplaceHelper.Implementations.ReplaceHelper(_options);
        
        var getTemplateReplaceQuery = new GetTemplateReplaceQuery
        {
            Replacements =
            {
                new GetTemplateReplacementQuery("ещё", [6]),
                new GetTemplateReplacementQuery("французских", [77]),
                new GetTemplateReplacementQuery("ещё", [171]),
                new GetTemplateReplacementQuery("булок", [199]),
                new GetTemplateReplacementQuery("чаю", [215, 270]),
                new GetTemplateReplacementQuery("французских", [242]),
            }
        };
        
        var result = replaceHelper.GetReplaceTemplate(Str1, getTemplateReplaceQuery);
        
        Assert.True(result == Test2ExpectedResult);
    }
    
    [Fact]
    public void Test3()
    {
        var replaceHelper = new ReplaceHelper.Implementations.ReplaceHelper(_options);
        
        var getStrReplaceQuery = new GetStrReplaceQuery
        (
            [
                new GetStrReplacementQuery("ещё6", "больше"),
                new GetStrReplacementQuery("французских77", "китайских"),
                new GetStrReplacementQuery("ещё171", "меньше"),
                new GetStrReplacementQuery("булок199", "круассанов"),
                new GetStrReplacementQuery("чаю215_270", "кофе"),
                new GetStrReplacementQuery("французских242", "российских")
            ]
        );
        
        var result = replaceHelper.GetStrByTemplate(Test2ExpectedResult, getStrReplaceQuery);
        
        Assert.True(result == Str2);
    }
}