using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IbanValidator.Specialized.Germany
{
    public class Bankleitzahl
    {
        public short ClearingArea { get; private set; }
        public Bankengruppe Bankengruppe { get; private set; }
        public short IndividualNumber { get; private set; }

        public Bankleitzahl(long blz)
        {
            ClearingArea = (short)(blz / 100000);
            Bankengruppe = (Bankengruppe)(blz / 10000 % 10);
            IndividualNumber = (short)(blz % 1000);
        }

        public static Bankleitzahl Parse(string blz)
        {
            return new Bankleitzahl(long.Parse(blz));
        }
    }
}
