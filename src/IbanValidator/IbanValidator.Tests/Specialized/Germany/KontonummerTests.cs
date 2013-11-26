using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IbanValidator.Specialized.Germany.Tests
{
    [TestClass]
    public class KontonummerTests
    {
        [TestMethod]
        public void KontonummerTest()
        {
            var kto = new Kontonummer(123456789);
            Assert.AreEqual(123456789, kto.Value);
        }

        [TestMethod]
        public void ParseTest()
        {
            var kto = Kontonummer.Parse("123456789");
            Assert.AreEqual(123456789, kto.Value);
        }
    }
}
