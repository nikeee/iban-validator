using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IbanValidator.Tests
{
    [TestClass]
    public class IbanTests
    {
        #region Contructor

        [TestMethod]
        public void IbanTest()
        {
            var iban = new Iban("DE", 68, "210501700012345678");
            Assert.AreEqual("DE", iban.CountryCode);
            Assert.AreEqual(68, iban.Checksum);
            Assert.AreEqual("210501700012345678", iban.Bban);
        }

        [TestMethod]
        public void IbanTest1()
        {
            var iban = new Iban("DE", 68, "2105 0170 0012 3456 78");
            Assert.AreEqual("DE", iban.CountryCode);
            Assert.AreEqual(68, iban.Checksum);
            Assert.AreEqual("210501700012345678", iban.Bban);
        }

        [TestMethod]
        public void IbanTest2()
        {
            var iban = new Iban("de", 68, "2105 0170 0012 3456 78");
            Assert.AreEqual("DE", iban.CountryCode);
            Assert.AreEqual(68, iban.Checksum);
            Assert.AreEqual("210501700012345678", iban.Bban);
        }

        #endregion
        #region IsValid

        [TestMethod]
        public void IsValid()
        {
            var iban = new Iban("DE", 68, "210501700012345678");
            Assert.IsTrue(iban.IsValid);
        }

        [TestMethod]
        public void IsValid1()
        {
            var iban = new Iban("DE", 68, "2105 0170 0012 3456 78");
            Assert.IsTrue(iban.IsValid);
        }

        [TestMethod]
        public void IsValid2()
        {
            var iban = new Iban("de", 68, "2105 0170 0012 3456 78");
            Assert.IsTrue(iban.IsValid);
        }

        [TestMethod]
        public void IsValid3()
        {
            var iban = new Iban("DE", 88, "200800000970375700");
            Assert.IsTrue(iban.IsValid);
        }

        [TestMethod]
        public void IsValid4()
        {
            var iban = new Iban("DE", 88, "2008 0000 0970 3757 00");
            Assert.IsTrue(iban.IsValid);
        }

        [TestMethod]
        public void IsValid5()
        {
            var iban = new Iban("de", 88, "2008 0000 0970 3757 00");
            Assert.IsTrue(iban.IsValid);
        }

        [TestMethod]
        public void IsValid6()
        {
            var iban = new Iban("DE", 88, "200800000970375710");
            Assert.IsFalse(iban.IsValid);
        }

        [TestMethod]
        public void IsValid7()
        {
            var iban = new Iban("DE", 88, "2008 0000 0970 3757 10");
            Assert.IsFalse(iban.IsValid);
        }

        [TestMethod]
        public void IsValid8()
        {
            var iban = new Iban("de", 88, "2008 0000 0970 3757 10");
            Assert.IsFalse(iban.IsValid);
        }

        #endregion
        #region ToString

        [TestMethod]
        public void ToString1()
        {
            var iban = new Iban("de", 68, "210501700012345678");
            Assert.AreEqual("DE68 2105 0170 0012 3456 78", iban.ToString());
        }

        [TestMethod]
        public void ToString2()
        {
            var iban = new Iban("de", 68, "210501700012345678");
            Assert.AreEqual("DE68 2105 0170 0012 3456 78", iban.ToString());
        }

        [TestMethod]
        public void ToString3()
        {
            var iban = new Iban("de", 88, "2008 0000 0970 3757 10");
            Assert.AreEqual("DE88 2008 0000 0970 3757 10", iban.ToString());
        }

        [TestMethod]
        public void ToString4()
        {
            var iban = new Iban("de", 88, "200800000970375710");
            Assert.AreEqual("DE88 2008 0000 0970 3757 10", iban.ToString());
        }

        #endregion
        #region Parse

        [TestMethod]
        public void Parse()
        {
            var iban = Iban.Parse("DE88 2008 0000 0970 3757 10");
            Assert.AreEqual("DE88 2008 0000 0970 3757 10", iban.ToString());
        }

        [TestMethod]
        public void Parse1()
        {
            var iban = Iban.Parse("DE88200800000970375710");
            Assert.AreEqual("DE88 2008 0000 0970 3757 10", iban.ToString());
        }

        [TestMethod]
        public void Parse2()
        {
            var iban = Iban.Parse("D E882 008000 009703 75710");
            Assert.AreEqual("DE88 2008 0000 0970 3757 10", iban.ToString());
        }

        #endregion
        #region TryParse
        
        [TestMethod]
        public void TryParse()
        {
            Iban iban;
            bool success = Iban.TryParse("DE88 2008 0000 0970 3757 10", out iban);
            Assert.AreEqual(true, success);
            Assert.AreNotEqual(null, iban);
            Assert.AreEqual("DE88 2008 0000 0970 3757 10", iban.ToString());
        }

        [TestMethod]
        public void TryParse1()
        {
            Iban iban;
            bool success = Iban.TryParse("DE88200800000970375710", out iban);
            Assert.AreEqual(true, success);
            Assert.AreNotEqual(null, iban);
            Assert.AreEqual("DE88 2008 0000 0970 3757 10", iban.ToString());
        }

        [TestMethod]
        public void TryParse2()
        {
            Iban iban;
            bool success = Iban.TryParse("D E882 008000 009703 75710", out iban);
            Assert.AreEqual(true, success);
            Assert.AreNotEqual(null, iban);
            Assert.AreEqual("DE88 2008 0000 0970 3757 10", iban.ToString());
        }


        [TestMethod]
        public void TryParse3()
        {
            Iban iban;
            bool success = Iban.TryParse("D88 2008 0000 0970 3757 10", out iban);
            Assert.AreEqual(false, success);
            Assert.AreEqual(null, iban);
        }

        [TestMethod]
        public void TryParse4()
        {
            Iban iban;
            bool success = Iban.TryParse("D88200800000970375710", out iban);
            Assert.AreEqual(false, success);
            Assert.AreEqual(null, iban);
        }

        [TestMethod]
        public void TryParse5()
        {
            Iban iban;
            bool success = Iban.TryParse("D 882 008000 009703 75710", out iban);
            Assert.AreEqual(false, success);
            Assert.AreEqual(null, iban);
        }
        #endregion
    }
}
