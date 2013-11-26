using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IbanValidator.Specialized.Germany.Tests
{
    [TestClass()]
    public class BankleitzahlTests
    {
        [TestMethod]
        public void BankleitzahlTest()
        {
            var blz = new Bankleitzahl(64090100);
            Assert.AreEqual(Bankengruppe.Volksbank, blz.Bankengruppe);
            Assert.AreEqual(640, blz.ClearingArea);
            Assert.AreEqual(0100, blz.IndividualNumber);
        }

        [TestMethod]
        public void ParseTest()
        {
            var blz = Bankleitzahl.Parse("64090100");
            Assert.AreEqual(64090100, blz.Value);
        }
    }
}
