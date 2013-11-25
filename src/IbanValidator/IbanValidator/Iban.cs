using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

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
                return _isValid;
            }
        }

        public Iban(string countryCode, byte checksum, string bban)
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

            bban = bban.ToUpperInvariant();
            if (!ValidateBban(bban))
                throw new ArgumentException("Invalid bban.");

            _bban = bban;

            _isValid = ValidateNumber();
        }

        private bool ValidateNumber()
        {
            const int modValue = 97;
            const int maxLength = 9;
            const int checksumLength = 2;

            var wholeString = string.Concat(_bban, _countryCode, _checksum.ToString().PadLeft(checksumLength, '0'));

            var sb = new StringBuilder();
            for (int i = 0; i < wholeString.Length; ++i)
                sb.Append(wholeString[i].GetNumericValue());

            string valuedString = sb.ToString();
            long currentSum = 0;
            for (int i = 0; i < valuedString.Length; i += maxLength)
            {
                int subStrLength = Math.Min(i + 9, valuedString.Length - i);
                string substr = valuedString.Substring(i, subStrLength);

                if (substr.Length < maxLength)
                {
                    int charsToAdd = maxLength - substr.Length;
                    string prefix = currentSum.ToString().PadLeft(charsToAdd, '0');
                    substr = prefix + substr;
                }

                var currentValue = long.Parse(substr);
                currentSum = currentValue % modValue;
            }

            return currentSum % modValue == 1;
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
            if (bban.Length <= MaxBbanLength)
                return false;
            for (int i = 0; i < bban.Length; ++i)
                if (!char.IsDigit(bban[i]) && !bban[i].IsValidChar())
                    return false;
            return true;
        }
    }
}
