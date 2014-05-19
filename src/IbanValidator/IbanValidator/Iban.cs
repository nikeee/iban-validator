using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

#if NET40

using System.Numerics;

#endif

namespace IbanValidator
{
    public class Iban
    {
        private readonly string _countryCode;
        public string CountryCode { get { return _countryCode; } }

        private readonly byte _checksum;
        public byte Checksum { get { return _checksum; } }

        private readonly string _bban;
        public string Bban { get { return _bban; } }

        private readonly bool _isValid;
        public virtual bool IsValid
        {
            get
            {
                if (!_isValid)
                    return false;
                // If there is a bban validator available, we use it.
                return _bbanValidator == null && _bbanValidator.Validate(_bban);
            }
        }

        private readonly BbanValidator _bbanValidator;
        public BbanValidator BbanValidator { get { return _bbanValidator; } }

        public Iban(string countryCode, byte checksum, string bban)
            : this(countryCode, checksum, bban, null)
        { }

        public Iban(string countryCode, byte checksum, string bban, BbanValidator bbanValidator)
        {
            if (string.IsNullOrEmpty(countryCode))
                throw new ArgumentNullException("countryCode");

            countryCode = countryCode.ToUpperInvariant();
            if (!ValidateCountryCode(countryCode))
                throw new ArgumentException("countryCode is not a valid ISO 3166-1 country code.");

            _countryCode = countryCode;

            if (checksum > 99)
                throw new ArgumentException("Invalid checksum.");
            _checksum = checksum;

            if (string.IsNullOrEmpty(bban))
                throw new ArgumentNullException("bban");
#if NET20
            bban = StringExtensions.StripWhiteSpace(bban);
#else
            bban = bban.StripWhiteSpace();
#endif
            bban = bban.ToUpperInvariant();
            if (!ValidateBban(bban))
                throw new ArgumentException("Invalid bban.");

            _bban = bban;

            _isValid = ValidateCountrySpecific();
            if(_isValid)
                _isValid = ValidateNumber();

            _bbanValidator = bbanValidator;
        }

        private const int ChecksumLength = 2;
        private bool ValidateNumber()
        {
            const int modValue = 97;
            const int modResult = 1;

            var wholeString = string.Concat(_bban, _countryCode, _checksum.ToString(CultureInfo.InvariantCulture).PadLeft(ChecksumLength, '0'));

            var sb = new StringBuilder();
            for (int i = 0; i < wholeString.Length; ++i)
#if NET20
                sb.Append(CharExtensions.GetNumericValue(wholeString[i]));
#else
                sb.Append(wholeString[i].GetNumericValue());
#endif

            string valuedString = sb.ToString();

#if !NET40
            // Little workaround for not having a BigInteger class.

            const int maxLength = 9;

            long currentSum = 0;
            while (valuedString.Length > 0)
            {

                string nextString;
                int subStrLength;
                if (currentSum > 0)
                {
                    var sumStr = currentSum.ToString(CultureInfo.InvariantCulture);
                    subStrLength = maxLength - sumStr.Length;
                    subStrLength = Math.Min(subStrLength, valuedString.Length);
                    nextString = sumStr + valuedString.Substring(0, subStrLength);
                }
                else
                {
                    subStrLength = Math.Min(maxLength, valuedString.Length);
                    nextString = valuedString.Substring(0, subStrLength);
                }
                valuedString = valuedString.Remove(0, subStrLength);

                currentSum = long.Parse(nextString) % modValue;
            }
            return currentSum % modValue == modResult;
#else
            // Since .NET 4.0 and above have a BigInteger class, we use that.
            var ibanValue = BigInteger.Parse(valuedString);
            return ibanValue % modValue == modResult;
#endif
        }

        private bool ValidateCountrySpecific()
        {
            return CountryValidation.IsValidRest(_countryCode, _bban);
        }

        private static bool ValidateCountryCode(string countryCode)
        {
            countryCode = countryCode.Trim();
            return countryCode.Length == 2 && CountryValidation.IsValidCountryCode(countryCode);
        }

        private const int MaxBbanLength = 30;
        private static bool ValidateBban(string bban)
        {
            bban = bban.Trim();
            if (bban.Length > MaxBbanLength)
                return false;
            for (int i = 0; i < bban.Length; ++i)
            {
#if NET20
                if (!char.IsDigit(bban[i]) && !CharExtensions.IsValidChar(bban[i]))
#else
                if (!char.IsDigit(bban[i]) && !bban[i].IsValidChar())
#endif
                    return false;
            }
            return true;
        }

        private static readonly Regex ParsePattern = new Regex(@"^(?<country>[a-zA-Z]{2})(?<checksum>\d{2})(?<bban>[a-zA-Z\d]{1,30})$");
        public static Iban Parse(string iban)
        {
            if (string.IsNullOrEmpty(iban))
                throw new ArgumentNullException("iban");
#if NET20
            iban = StringExtensions.StripWhiteSpace(iban);
#else
            iban = iban.StripWhiteSpace();
#endif
            var m = ParsePattern.Match(iban);
            if (!m.Success)
                throw new FormatException("Invalid IBAN");

            var countryCode = m.Groups["country"].Value;
            var checksum = byte.Parse(m.Groups["checksum"].Value);
            var bban = m.Groups["bban"].Value;

            return new Iban(countryCode, checksum, bban);
        }

        public static bool TryParse(string iban, out Iban result)
        {
            result = null;
            if (string.IsNullOrEmpty(iban))
                return false;

#if NET20
            iban = StringExtensions.StripWhiteSpace(iban);
#else
            iban = iban.StripWhiteSpace();
#endif
            var m = ParsePattern.Match(iban);
            if (!m.Success)
                return false;

            var countryCode = m.Groups["country"].Value;
            if (string.IsNullOrEmpty(countryCode) || countryCode.Length != 2)
                return false;

            byte checksum;
            if (!byte.TryParse(m.Groups["checksum"].Value, out checksum))
                return false;
            if (checksum > 99)
                return false;

            var bban = m.Groups["bban"].Value;
            if (string.IsNullOrEmpty(bban) || bban.Length > MaxBbanLength)
                return false;

            result = new Iban(countryCode, checksum, bban);
            return true;
        }

        #region Equality
        
        public static bool operator ==(Iban a, Iban b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (((object)a == null) || ((object)b == null))
                return false;
            return a._checksum == b._checksum && a._countryCode == b._countryCode && a._bban == b._bban;
        }

        public static bool operator !=(Iban a, Iban b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return _countryCode.GetHashCode() ^ _checksum ^ _bban.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var iban = obj as Iban;
            if ((object)iban == null)
                return false;
            return base.Equals(obj) && this == iban;
        }

        public bool Equals(Iban iban)
        {
            if ((object)iban == null)
                return false;
            return this == iban;
        }

        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder(34);
            sb.Append(CountryCode);
            sb.Append(Checksum.ToString(CultureInfo.InvariantCulture).PadLeft(ChecksumLength, '0'));
            for (int i = 0; i < _bban.Length; ++i)
            {
                if (i % 4 == 0)
                    sb.Append(' ');
                sb.Append(_bban[i]);
            }
            return sb.ToString();
        }
    }
}
