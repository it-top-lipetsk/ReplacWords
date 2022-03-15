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
            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestReplaceNumberOfSubstitutions()
        {
            var words = new string[] { "df", "123", "qwerty" };
            var str = "Qwerty DF df 123, zxdd df123df";
            var replace = new Replace(words, str);

            replace.ReplacementWord();

            var expected = 7;
            var actual = replace.NumberOfSubstitutions;

            Assert.AreEqual(expected, actual);
            
        }
    }
}
