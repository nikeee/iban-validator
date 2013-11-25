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
            var iban = new Iban("DE", 68, "210501700012345678");
            Assert.AreEqual("DE", iban.CountryCode);
            Assert.AreEqual(68, iban.Checksum);
            Assert.AreEqual("210501700012345678", iban.Bban);
        }

        [TestMethod]
        public void Constructor2()
        {
            var iban = new Iban("DE", 68, "2105 0170 0012 3456 78");
            Assert.AreEqual("DE", iban.CountryCode);
            Assert.AreEqual(68, iban.Checksum);
            Assert.AreEqual("210501700012345678", iban.Bban);
        }

        [TestMethod]
        public void Constructor3()
        {
            var iban = new Iban("de", 68, "2105 0170 0012 3456 78");
            Assert.AreEqual("DE", iban.CountryCode);
            Assert.AreEqual(68, iban.Checksum);
            Assert.AreEqual("210501700012345678", iban.Bban);
        }

        [TestMethod]
        public void IsValid1()
        {
            var iban = new Iban("DE", 68, "210501700012345678");
            Assert.IsTrue(iban.IsValid);
        }

        [TestMethod]
        public void IsValid2()
        {
            var iban = new Iban("DE", 68, "2105 0170 0012 3456 78");
            Assert.IsTrue(iban.IsValid);
        }

        [TestMethod]
        public void IsValid3()
        {
            var iban = new Iban("de", 68, "2105 0170 0012 3456 78");
            Assert.IsTrue(iban.IsValid);
        }

        [TestMethod]
        public void IsValid4()
        {
            var iban = new Iban("DE", 88, "200800000970375700");
            Assert.IsTrue(iban.IsValid);
        }

        [TestMethod]
        public void IsValid5()
        {
            var iban = new Iban("DE", 88, "2008 0000 0970 3757 00");
            Assert.IsTrue(iban.IsValid);
        }
        
        [TestMethod]
        public void IsValid6()
        {
            var iban = new Iban("de", 88, "2008 0000 0970 3757 00");
            Assert.IsTrue(iban.IsValid);
        }

        [TestMethod]
        public void IsValid7()
        {
            var iban = new Iban("DE", 88, "200800000970375710");
            Assert.IsFalse(iban.IsValid);
        }

        [TestMethod]
        public void IsValid8()
        {
            var iban = new Iban("DE", 88, "2008 0000 0970 3757 10");
            Assert.IsFalse(iban.IsValid);
        }

        [TestMethod]
        public void IsValid9()
        {
            var iban = new Iban("de", 88, "2008 0000 0970 3757 10");
            Assert.IsFalse(iban.IsValid);
        }
    }
}
