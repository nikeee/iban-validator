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

        public Iban(string countryCode, byte checksum, string bban)
        {
            if (string.IsNullOrEmpty(countryCode))
                throw new ArgumentNullException("countryCode");
            countryCode = countryCode.ToUpperInvariant();
            if (!ValidateCountryCode(countryCode))
                throw new ArgumentException("countryCode is not a valid ISO 3166-1 country code.");

            CountryCode = countryCode;
        }


        private static bool ValidateCountryCode(string countryCode)
        {
            if (countryCode.Length != 2)
                return false;
        }
    }
}
