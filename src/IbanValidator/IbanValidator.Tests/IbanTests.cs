using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IbanValidator.Tests
{
    [TestClass]
    public class IbanTests
    {
        [TestMethod]
        public void Constructor1()
        {
            var iban1 = new Iban("DE", 68, "210501700012345678");
            Assert.AreEqual("DE", iban1.CountryCode);
            Assert.AreEqual(68, iban1.Checksum);
            Assert.AreEqual("210501700012345678", iban1.Bban);
        }
    }
}
