using NUnit.Framework;

namespace IbanValidator.Tests
{
    [TestFixture]
    public class IbanTests
    {
        #region Contructor

        [Test]
        public void IbanTest()
        {
            var iban = new Iban("DE", 68, "210501700012345678");
            Assert.AreEqual("DE", iban.CountryCode);
            Assert.AreEqual(68, iban.Checksum);
            Assert.AreEqual("210501700012345678", iban.Bban);
        }

        [Test]
        public void IbanTest1()
        {
            var iban = new Iban("DE", 68, "2105 0170 0012 3456 78");
            Assert.AreEqual("DE", iban.CountryCode);
            Assert.AreEqual(68, iban.Checksum);
            Assert.AreEqual("210501700012345678", iban.Bban);
        }

        [Test]
        public void IbanTest2()
        {
            var iban = new Iban("de", 68, "2105 0170 0012 3456 78");
            Assert.AreEqual("DE", iban.CountryCode);
            Assert.AreEqual(68, iban.Checksum);
            Assert.AreEqual("210501700012345678", iban.Bban);
        }

        #endregion
        #region IsValid

        [Test]
        public void IsValid()
        {
            var iban = new Iban("DE", 68, "210501700012345678");
            Assert.IsTrue(iban.IsValid);
        }

        [Test]
        public void IsValid1()
        {
            var iban = new Iban("DE", 68, "2105 0170 0012 3456 78");
            Assert.IsTrue(iban.IsValid);
        }

        [Test]
        public void IsValid2()
        {
            var iban = new Iban("de", 68, "2105 0170 0012 3456 78");
            Assert.IsTrue(iban.IsValid);
        }

        [Test]
        public void IsValid3()
        {
            var iban = new Iban("DE", 88, "200800000970375700");
            Assert.IsTrue(iban.IsValid);
        }

        [Test]
        public void IsValid4()
        {
            var iban = new Iban("DE", 88, "2008 0000 0970 3757 00");
            Assert.IsTrue(iban.IsValid);
        }

        [Test]
        public void IsValid5()
        {
            var iban = new Iban("de", 88, "2008 0000 0970 3757 00");
            Assert.IsTrue(iban.IsValid);
        }

        [Test]
        public void IsValid6()
        {
            var iban = new Iban("DE", 88, "200800000970375710");
            Assert.IsFalse(iban.IsValid);
        }

        [Test]
        public void IsValid7()
        {
            var iban = new Iban("DE", 88, "2008 0000 0970 3757 10");
            Assert.IsFalse(iban.IsValid);
        }

        [Test]
        public void IsValid8()
        {
            var iban = new Iban("de", 88, "2008 0000 0970 3757 10");
            Assert.IsFalse(iban.IsValid);
        }

        #endregion
        #region ToString

        [Test]
        public void ToString1()
        {
            var iban = new Iban("de", 68, "210501700012345678");
            Assert.AreEqual("DE68 2105 0170 0012 3456 78", iban.ToString());
        }

        [Test]
        public void ToString2()
        {
            var iban = new Iban("de", 68, "210501700012345678");
            Assert.AreEqual("DE68 2105 0170 0012 3456 78", iban.ToString());
        }

        [Test]
        public void ToString3()
        {
            var iban = new Iban("de", 88, "2008 0000 0970 3757 10");
            Assert.AreEqual("DE88 2008 0000 0970 3757 10", iban.ToString());
        }

        [Test]
        public void ToString4()
        {
            var iban = new Iban("de", 88, "200800000970375710");
            Assert.AreEqual("DE88 2008 0000 0970 3757 10", iban.ToString());
        }

        #endregion
        #region Parse

        [Test]
        public void Parse()
        {
            var iban = Iban.Parse("DE88 2008 0000 0970 3757 10");
            Assert.AreEqual("DE88 2008 0000 0970 3757 10", iban.ToString());
        }

        [Test]
        public void Parse1()
        {
            var iban = Iban.Parse("DE88200800000970375710");
            Assert.AreEqual("DE88 2008 0000 0970 3757 10", iban.ToString());
        }

        [Test]
        public void Parse2()
        {
            var iban = Iban.Parse("D E882 008000 009703 75710");
            Assert.AreEqual("DE88 2008 0000 0970 3757 10", iban.ToString());
        }

        #endregion
        #region TryParse

        [Test]
        public void TryParse()
        {
            Iban iban;
            bool success = Iban.TryParse("DE88 2008 0000 0970 3757 10", out iban);
            Assert.AreEqual(true, success);
            Assert.AreNotEqual(null, iban);
            Assert.AreEqual("DE88 2008 0000 0970 3757 10", iban.ToString());
        }

        [Test]
        public void TryParse1()
        {
            Iban iban;
            bool success = Iban.TryParse("DE88200800000970375710", out iban);
            Assert.AreEqual(true, success);
            Assert.AreNotEqual(null, iban);
            Assert.AreEqual("DE88 2008 0000 0970 3757 10", iban.ToString());
        }

        [Test]
        public void TryParse2()
        {
            Iban iban;
            bool success = Iban.TryParse("D E882 008000 009703 75710", out iban);
            Assert.AreEqual(true, success);
            Assert.AreNotEqual(null, iban);
            Assert.AreEqual("DE88 2008 0000 0970 3757 10", iban.ToString());
        }


        [Test]
        public void TryParse3()
        {
            Iban iban;
            bool success = Iban.TryParse("D88 2008 0000 0970 3757 10", out iban);
            Assert.AreEqual(false, success);
            Assert.AreEqual(null, iban);
        }

        [Test]
        public void TryParse4()
        {
            Iban iban;
            bool success = Iban.TryParse("D88200800000970375710", out iban);
            Assert.AreEqual(false, success);
            Assert.AreEqual(null, iban);
        }

        [Test]
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
