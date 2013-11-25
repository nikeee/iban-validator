
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IbanValidator
{
    public abstract class BbanValidator
    {
        public BbanValidator()
        { }

        public abstract bool Validate(string bban);
    }
}
