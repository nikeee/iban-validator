using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IbanValidator
{
    public abstract class IbanInformationProvider
    {

        protected readonly Iban _iban;
        public Iban Iban { get { return _iban; } }

        public IbanInformationProvider(Iban iban)
        {
            if (iban == null)
                throw new ArgumentNullException("iban");
            _iban = iban;
        }
    }

    public class GermanyIbanInformationProvider : IbanInformationProvider
    {
        private readonly Bankleitzahl _blz;
        public Bankleitzahl Bankleitzahl { get { return _blz; } }
        public GermanyIbanInformationProvider(Iban iban)
            : base(iban)
        {

        }
    }

    public class Bankleitzahl
    {
        public short ClearingArea { get; private set; }
        public Bankengruppe Bankengruppe {get; private set;}

        public Bankleitzahl(long blz)
        {
            ClearingArea = (short)(blz / 100000);
            Bankengruppe = (Bankengruppe)(blz / 10000 % 10);
        }

        public static Bankleitzahl Parse(string blz)
        {
            var blzLong = long.Parse(blz);
            return new Bankleitzahl(blzLong);
        }
    }

    public enum Bankengruppe : byte
    {
        DeutscheBundesbank = 0,
        MiscAndDeutschePostbankAg = 1,
        Misc2 = 2,
        Misc3 = 3,
        Commerzbank1 = 4,
        SparkasseOrLandesbank = 5,
        GenossenschaftlicheZentralbankAndRaiffeisenbank = 6,
        DeutscheBank = 7,
        Commerzbank2 = 8,
        Volksbanken = 9
    }
}
