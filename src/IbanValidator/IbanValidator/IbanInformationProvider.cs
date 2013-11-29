﻿using System;

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
}
