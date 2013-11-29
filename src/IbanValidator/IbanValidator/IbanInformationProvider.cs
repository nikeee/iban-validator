using System;

namespace IbanValidator
{
    public abstract class IbanInformationProvider
    {

        protected readonly Iban IbanInteral;
        public Iban Iban { get { return IbanInteral; } }

        protected IbanInformationProvider(Iban iban)
        {
            if (iban == null)
                throw new ArgumentNullException("iban");
            IbanInteral = iban;
        }
    }
}
