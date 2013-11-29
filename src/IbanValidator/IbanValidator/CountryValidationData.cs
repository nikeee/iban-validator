using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

#if !NET20

using System.Linq;

#endif

namespace IbanValidator
{
    // Thanks to:
    // http://www.codeproject.com/Articles/55667/IBAN-Verification-in-C-Excel-Automation-Add-in-Wor

    internal class CountryValidationData
    {
        private readonly string _countryCode;
        public string CountryCode { get { return _countryCode; } }

        private readonly int _ibanLength;
        public int IbanLength { get { return _ibanLength; } }

        private readonly Regex _validationPattern;
        public Regex ValidationPattern { get { return _validationPattern; } }

        private readonly bool _isEu924;
        public bool IsEu924 { get { return _isEu924; } }

        private readonly string _sample;
        public string Sample { get { return _sample; } }

        public CountryValidationData(string countryCode, int ibanLength, string validationPattern, bool isEu924, string sample)
        {
            if (countryCode == null || countryCode.Length != 2)
                throw new ArgumentException("Invalid country code.");
            _countryCode = countryCode;
            _ibanLength = ibanLength;
            _validationPattern = new Regex(validationPattern);
            _isEu924 = isEu924;
            _sample = sample;
        }
    }

    internal static class CountryValidation
    {
        private static List<CountryValidationData> _validationDataList;

        private static void InitializeCountryData()
        {
            _validationDataList = new List<CountryValidationData>();
            _validationDataList.AddRange(
            new[] {
                new CountryValidationData("AD", 24, @"\d{8}[a-zA-Z0-9]{12}", false, "AD1200012030200359100100"),
                new CountryValidationData("AL", 28, @"\d{8}[a-zA-Z0-9]{16}", false, "AL47212110090000000235698741"),
                new CountryValidationData("AT", 20, @"\d{16}", true, "AT611904300234573201"),
                new CountryValidationData("BA", 20, @"\d{16}", false, "BA391290079401028494"),
                new CountryValidationData("BE", 16, @"\d{12}", true, "BE68539007547034"),
                new CountryValidationData("BG", 22, @"[A-Z]{4}\d{6}[a-zA-Z0-9]{8}", true, "BG80BNBG96611020345678"),
                new CountryValidationData("CH", 21, @"\d{5}[a-zA-Z0-9]{12}", false, "CH9300762011623852957"),
                new CountryValidationData("CY", 28, @"\d{8}[a-zA-Z0-9]{16}", true, "CY17002001280000001200527600"),
                new CountryValidationData("CZ", 24, @"\d{20}", true, "CZ6508000000192000145399"),
                new CountryValidationData("DE", 22, @"\d{18}", true, "DE89370400440532013000"),
                new CountryValidationData("DK", 18, @"\d{14}", true, "DK5000400440116243"),
                new CountryValidationData("EE", 20, @"\d{16}", true, "EE382200221020145685"),
                new CountryValidationData("ES", 24, @"\d{20}", true, "ES9121000418450200051332"),
                new CountryValidationData("FI", 18, @"\d{14}", true, "FI2112345600000785"),
                new CountryValidationData("FO", 18, @"\d{14}", false, "FO6264600001631634"),
                new CountryValidationData("FR", 27, @"\d{10}[a-zA-Z0-9]{11}\d\d", true, "FR1420041010050500013M02606"),
                new CountryValidationData("GB", 22, @"[A-Z]{4}\d{14}", true, "GB29NWBK60161331926819"),
                new CountryValidationData("GI", 23, @"[A-Z]{4}[a-zA-Z0-9]{15}", true, "GI75NWBK000000007099453"),
                new CountryValidationData("GL", 18, @"\d{14}", false, "GL8964710001000206"),
                new CountryValidationData("GR", 27, @"\d{7}[a-zA-Z0-9]{16}", true, "GR1601101250000000012300695"),
                new CountryValidationData("HR", 21, @"\d{17}", false, "HR1210010051863000160"),
                new CountryValidationData("HU", 28, @"\d{24}", true, "HU42117730161111101800000000"),
                new CountryValidationData("IE", 22, @"[A-Z]{4}\d{14}", true, "IE29AIBK93115212345678"),
                new CountryValidationData("IL", 23, @"\d{19}", false, "IL620108000000099999999"),
                new CountryValidationData("IS", 26, @"\d{22}", true, "IS140159260076545510730339"),
                new CountryValidationData("IT", 27, @"[A-Z]\d{10}[a-zA-Z0-9]{12}", true, "IT60X0542811101000000123456"),
                new CountryValidationData("LB", 28, @"\d{4}[a-zA-Z0-9]{20}", false, "LB62099900000001001901229114"),
                new CountryValidationData("LI", 21, @"\d{5}[a-zA-Z0-9]{12}", true, "LI21088100002324013AA"),
                new CountryValidationData("LT", 20, @"\d{16}", true, "LT121000011101001000"),
                new CountryValidationData("LU", 20, @"\d{3}[a-zA-Z0-9]{13}", true, "LU280019400644750000"),
                new CountryValidationData("LV", 21, @"[A-Z]{4}[a-zA-Z0-9]{13}", true, "LV80BANK0000435195001"),
                new CountryValidationData("MC", 27, @"\d{10}[a-zA-Z0-9]{11}\d\d", true, "MC1112739000700011111000h79"),
                new CountryValidationData("ME", 22, @"\d{18}", false, "ME25505000012345678951"),
                new CountryValidationData("MK", 19, @"\d{3}[a-zA-Z0-9]{10}\d\d", false, "MK07300000000042425"),
                new CountryValidationData("MT", 31, @"[A-Z]{4}\d{5}[a-zA-Z0-9]{18}", true, "MT84MALT011000012345MTLCAST001S"),
                new CountryValidationData("MU", 30, @"[A-Z]{4}\d{19}[A-Z]{3}", false, "MU17BOMM0101101030300200000MUR"),
                new CountryValidationData("NL", 18, @"[A-Z]{4}\d{10}", true, "NL91ABNA0417164300"),
                new CountryValidationData("NO", 15, @"\d{11}", true, "NO9386011117947"),
                new CountryValidationData("PL", 28, @"\d{8}[a-zA-Z0-9]{16}", true, "PL27114020040000300201355387"),
                new CountryValidationData("PT", 25, @"\d{21}", true, "PT50000201231234567890154"),
                new CountryValidationData("RO", 24, @"[A-Z]{4}[a-zA-Z0-9]{16}", true, "RO49AAAA1B31007593840000"),
                new CountryValidationData("RS", 22, @"\d{18}", false, "RS35260005601001611379"),
                new CountryValidationData("SA", 24, @"\d{2}[a-zA-Z0-9]{18}", false, "SA0380000000608010167519"),
                new CountryValidationData("SE", 24, @"\d{20}", true, "SE4550000000058398257466"),
                new CountryValidationData("SI", 19, @"\d{15}", true, "SI56191000000123438"),
                new CountryValidationData("SK", 24, @"\d{20}", true, "SK3112000000198742637541"),
                new CountryValidationData("SM", 27, @"[A-Z]\d{10}[a-zA-Z0-9]{12}", false, "SM86U0322509800000000270100"),
                new CountryValidationData("TN", 24, @"\d{20}", false, "TN5914207207100707129648"),
                new CountryValidationData("TR", 26, @"\d{5}[a-zA-Z0-9]{17}", false, "TR330006100519786457841326")
            });
        }

        public static bool IsValidCountryCode(string cc)
        {
            if (_validationDataList == null)
                InitializeCountryData();
            Debug.Assert(_validationDataList != null, "_validationDataList != null");
#if NET20
            for(int i = 0; i < _validationDataList.Count; ++i)
                if(_validationDataList[i].CountryCode == cc)
                    return true;
            return false;
#else
            //return _validationDataList.Any(c => c.CountryCode.IndexOf(cc, StringComparison.InvariantCultureIgnoreCase) == 0);
            return _validationDataList.Any(c => c.CountryCode == cc);
#endif
        }

        public static bool IsValidRest(string countryCode, string rest)
        {
            if (_validationDataList == null)
                InitializeCountryData();
            
            Debug.Assert(_validationDataList != null, "_validationDataList != null");

#if NET20
            CountryValidationData validator = null;
            for(int i = 0; i < _validationDataList.Count; ++i)
            {
                if(_validationDataList[i].CountryCode == countryCode)
                {
                    validator = _validationDataList[i];
                    break;
                }
            }
#else
            var validator = _validationDataList.FirstOrDefault(cvd => cvd.CountryCode == countryCode);
#endif

            if (validator == null)
                return false;
            return validator.ValidationPattern.IsMatch(rest);
        }
    }
}
