using ReplaceHelper.Interfaces;
using ReplaceHelper.Models.Query;

namespace ReplaceHelperTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var replaceHelperOptions = new ReplaceHelperOptions();
        var replaceHelper = new ReplaceHelper.Implementations.ReplaceHelper(replaceHelperOptions);
        
        var str1 = "привет я думаю что раз я три";
        var str2 = "привет он полагает что раз он три";

        var result1 = replaceHelper.GetReplaceHarvest(str1, str2);
    }
    
    [Fact]
    public void Test2()
    {
        var replaceHelperOptions = new ReplaceHelperOptions();
        var replaceHelper = new ReplaceHelper.Implementations.ReplaceHelper(replaceHelperOptions);
        
        var str = "привет я думаю что раз два три привет я думаю что раз два три привет я думаю что раз два три";
        var replaceTemplateQuery = new ReplaceTemplateQuery
        {
            Replacements =
            {
                new ReplacementQuery("привет", [31, 62]),
                new ReplacementQuery("три", [89]),
                new ReplacementQuery("раз", [])
            }
        };
        
        var result1 = replaceHelper.GetReplaceTemplate(str, replaceTemplateQuery);
    }
}