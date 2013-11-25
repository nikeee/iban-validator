using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#if NET4

using System.Numerics;

#endif

namespace IbanValidator
{
    public class Iban
    {
        protected readonly string _countryCode;
        public string CountryCode { get { return _countryCode; } }

        protected readonly byte _checksum;
        public byte Checksum { get { return _checksum; } }

        protected readonly string _bban;
        public string Bban { get { return _bban; } }

        private readonly bool _isValid;
        public virtual bool IsValid
        {
            get
            {
                if (!_isValid)
                    return false;
                // If there is a bban validator available, we use it.
                return _bbanValidator == null ? true : _bbanValidator.Validate(_bban);
            }
        }

        protected readonly BbanValidator _bbanValidator;
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

            bban = bban.StripWhiteSpace();
            bban = bban.ToUpperInvariant();
            if (!ValidateBban(bban))
                throw new ArgumentException("Invalid bban.");

            _bban = bban;

            _isValid = ValidateNumber();

            _bbanValidator = bbanValidator;
        }

        private const int ChecksumLength = 2;
        private bool ValidateNumber()
        {
            const int modValue = 97;
            const int modResult = 1;

            var wholeString = string.Concat(_bban, _countryCode, _checksum.ToString().PadLeft(ChecksumLength, '0'));

            var sb = new StringBuilder();
            for (int i = 0; i < wholeString.Length; ++i)
                sb.Append(wholeString[i].GetNumericValue());

            string valuedString = sb.ToString();

#if !NET4
            // Little workaround for not having a BigInteger class.

            const int maxLength = 9;

            long currentSum = 0;
            while (valuedString.Length > 0)
            {

                string nextString;
                int subStrLength;
                if (currentSum > 0)
                {
                    var sumStr = currentSum.ToString();
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


        private static bool ValidateCountryCode(string countryCode)
        {
            countryCode = countryCode.Trim();
            if (countryCode.Length != 2)
                return false;
            // TODO: Check against table
            return true;
        }

        private const int MaxBbanLength = 30;
        private static bool ValidateBban(string bban)
        {
            bban = bban.Trim();
            if (bban.Length > MaxBbanLength)
                return false;
            for (int i = 0; i < bban.Length; ++i)
                if (!char.IsDigit(bban[i]) && !bban[i].IsValidChar())
                    return false;
            return true;
        }

        private static Regex _parsePattern = new Regex(@"^(?<country>\w{2})(?<checksum>\d{2})(?<bban>\d{1,30})$");
        public static Iban Parse(string iban)
        {
            if (string.IsNullOrEmpty(iban))
                throw new ArgumentNullException("iban");
            var m = _parsePattern.Match(iban);
            if (!m.Success)
                throw new FormatException("Invalid IBAN");

            var countryCode = m.Groups["country"].Value;
            var checksum = byte.Parse(m.Groups["checksum"].Value);
            var bban = m.Groups["rest"].Value;

            return new Iban(countryCode, checksum, bban);
        }

        public override string ToString()
        {
            var sb = new StringBuilder(34);
            sb.Append(CountryCode);
            sb.Append(Checksum.ToString().PadLeft(ChecksumLength, '0'));

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
