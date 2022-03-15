using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReplacWords.Lib;

namespace TestProjectReplace
{
    [TestClass]
    public class UnitTestReplace
    {
        [TestMethod]
        public void TestReplace()
        {
            var words = new string[] { "df", "123", "qwerty" };
            var str = "Qwerty DF df 123, zxdd df123df";
            var replace = new Replace(words, str);

            var expected = "****** ** ** ***, zxdd *******";
            var actual = replace.ReplacementWord();
            var countExpected = 7;
            var actualExpected = replace.NumberOfSubstitutions;

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(countExpected, actualExpected);
        }
    }
}
