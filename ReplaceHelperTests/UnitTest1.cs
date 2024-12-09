using ReplaceHelper.Interfaces;

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

        var result = replaceHelper.GetReplaceHarvest(str1, str2);
        
        return;
    }
}