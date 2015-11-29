using System;

namespace IbanValidator.Specialized.Germany
{
    public class GermanyIbanInformationProvider : IbanInformationProvider
    {
        public Bankleitzahl Bankleitzahl { get; }
        public Kontonummer Kontonummer { get; }

        public GermanyIbanInformationProvider(Iban iban)
            : base(iban)
        {
            const int blzLength = 8;
            const int ktoLength = 8;

            // DE00 2105 0170 0012 3456 78
            //      210 501 70 - 0012 3456 78
            // DEpp bbb bbb bb - kkkk kkkk kk
            //      bbb bbb bb - kkkk kkkk kk (18 chars)

            var bbstr = iban.Bban;
            if (bbstr.Length != 18)
                throw new ArgumentException("Not a German IBAN.");

            var blzStr = bbstr.Substring(0, blzLength);
            var ktoStr = bbstr.Substring(18 - ktoLength, ktoLength);

            Bankleitzahl = Bankleitzahl.Parse(blzStr);
            Kontonummer = Kontonummer.Parse(ktoStr);
        }
    }
}
