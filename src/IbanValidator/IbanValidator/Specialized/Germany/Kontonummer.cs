using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IbanValidator.Specialized.Germany
{
    public class Kontonummer
    {
        public long Value { get; private set; }
        public Kontonummer(long kto)
        {
            if (kto > 999999999 || kto < 10000000)
                throw new ArgumentException("Invalid kto");
            Value = kto;
        }

        public static Kontonummer Parse(string kto)
        {
            return new Kontonummer(long.Parse(kto));
        }
    }
}
