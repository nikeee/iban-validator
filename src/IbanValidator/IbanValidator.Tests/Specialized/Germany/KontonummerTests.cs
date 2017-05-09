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

        [Test]
        public void WellKnownKto()
        {
            // See: https://github.com/nikeee/iban-validator/pull/5
            var kto = Kontonummer.Parse("1006410");
            Assert.AreEqual(1006410, kto.Value);
            var blz = Bankleitzahl.Parse("50050000");

            var success = Iban.TryParse("DE95 5005 0000 0001 006410", out var i);
            Assert.True(success);
            Assert.NotNull(i);
            Assert.True(i.IsValid);
            Assert.AreEqual("500500000001006410", i.Bban);
            var giip = new GermanyIbanInformationProvider(i);
            Assert.NotNull(giip);
            Assert.NotNull(giip.Kontonummer);
            Assert.AreEqual(kto, giip.Kontonummer);
            Assert.NotNull(giip.Bankleitzahl);
            // Assert.AreEqual(blz, giip.Bankleitzahl);

        }
    }
}
