using NUnit.Framework;

namespace IbanValidator.Specialized.Germany.Tests
{
    [TestFixture]
    public class KontonummerTests
    {
        [Test]
        public void KontonummerTest()
        {
            var kto = new Kontonummer(123456789);
            Assert.AreEqual(123456789, kto.Value);
        }

        [Test]
        public void ParseTest()
        {
            var kto = Kontonummer.Parse("123456789");
            Assert.AreEqual(123456789, kto.Value);
        }
    }
}
