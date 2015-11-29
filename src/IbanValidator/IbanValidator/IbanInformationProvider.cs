using System;

namespace IbanValidator
{
    public abstract class IbanInformationProvider
    {
        protected readonly Iban IbanInteral;
        public Iban Iban => IbanInteral;

        protected IbanInformationProvider(Iban iban)
        {
            if (iban == null)
                throw new ArgumentNullException(nameof(iban));
            IbanInteral = iban;
        }
    }
}
