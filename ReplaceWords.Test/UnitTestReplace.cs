using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReplacWords.Lib;

namespace ReplaceWords.Test;

[TestClass]
public class UnitTestReplace
{
    [TestMethod]
    public void TestReplace()
    {
        var words = new string[] { "df", "123", "qwerty" };
        var str = "Qwerty DF df 123 df123df";
        var replace = new Replace(words, str);
        
        var expected = "****** ** ** *** df123df";
        var actual = replace.ReplacementWord();
        
        Assert.AreEqual(expected, actual);
    }
}