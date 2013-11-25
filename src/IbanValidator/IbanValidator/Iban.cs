using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace IbanValidator
{
    public class Iban
    {
        public string CountryCode { get; private set; }
        public byte Checksum { get; private set; }

        public Iban(string countryCode, byte checksum, string bban)
        {
            if (string.IsNullOrEmpty(countryCode))
                throw new ArgumentNullException("countryCode");

            countryCode = countryCode.ToUpperInvariant();
            if (!ValidateCountryCode(countryCode))
                throw new ArgumentException("countryCode is not a valid ISO 3166-1 country code.");

            CountryCode = countryCode;

            if (checksum > 99)
                throw new ArgumentException("Invalid checksum.");
            Checksum = checksum;

            if (string.IsNullOrEmpty(bban))
                throw new ArgumentNullException("bban");
            bban = bban.ToUpperInvariant();


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
            {
                if (!char.IsDigit(bban[i]) && !char.IsXyz(bban[i]))
                    return false;
            }

            return true;
        }
    }
}
