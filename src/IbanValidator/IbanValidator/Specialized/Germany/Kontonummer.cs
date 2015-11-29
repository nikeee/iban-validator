using System;

namespace IbanValidator.Specialized.Germany
{
    public class Kontonummer
    {
        public long Value { get; }
        public Kontonummer(long kto)
        {
            if (kto > 999999999 || kto < 10000000)
                throw new ArgumentException($"Invalid {kto}");
            Value = kto;
        }

        public static Kontonummer Parse(string kto) => new Kontonummer(long.Parse(kto));
        public static bool TryParse(string blz, out Kontonummer result)
        {
            result = null;
            long parsedKto;
            if (long.TryParse(blz, out parsedKto))
                result = new Kontonummer(parsedKto);
            return result != null;
        }
    }
}
