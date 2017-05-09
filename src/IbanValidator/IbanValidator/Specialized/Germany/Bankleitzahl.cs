
using System;

namespace IbanValidator.Specialized.Germany
{
    public class Bankleitzahl : IEquatable<Bankleitzahl>
    {
        public short ClearingArea { get; }
        public Bankengruppe Bankengruppe { get; }
        public short IndividualNumber { get; }

        public long Value { get; }

        public Bankleitzahl(long blz)
        {
            Value = blz;
            ClearingArea = (short)(blz / 100000);
            Bankengruppe = (Bankengruppe)(blz / 10000 % 10);
            IndividualNumber = (short)(blz % 1000);
        }

        public static Bankleitzahl Parse(string blz) => new Bankleitzahl(long.Parse(blz));
        public static bool TryParse(string blz, out Bankleitzahl result)
        {
            result = null;
            if (long.TryParse(blz, out long parsedBlz))
                result = new Bankleitzahl(parsedBlz);
            return result != null;
        }

        public bool Equals(Bankleitzahl other)
        {
            return other.Bankengruppe == Bankengruppe
                && other.ClearingArea == ClearingArea
                && other.IndividualNumber == IndividualNumber
                && other.Value == Value;
        }
    }
}
