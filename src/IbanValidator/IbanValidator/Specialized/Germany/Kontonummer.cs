﻿using System;

namespace IbanValidator.Specialized.Germany
{
    public class Kontonummer
    {
        public long Value { get; }
        public Kontonummer(long kto)
        {
            if (kto > 9999999999 || kto < 1)
                throw new ArgumentException($"Invalid {nameof(kto)}");
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
