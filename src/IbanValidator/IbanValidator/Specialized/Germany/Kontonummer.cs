using System;

namespace IbanValidator.Specialized.Germany
{
    public class Kontonummer : IEquatable<Kontonummer>
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
            if (long.TryParse(blz, out long parsedKto))
                result = new Kontonummer(parsedKto);
            return result != null;
        }

        public override int GetHashCode() => Value.GetHashCode();
        public override bool Equals(object obj) => obj is Kontonummer kto ? Equals(kto) : false;
        public bool Equals(Kontonummer other) => other.Value == Value;

        public static bool operator ==(Kontonummer a, Kontonummer b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if ((object)a == null)
                return (object)b == null;
            else if ((object)b == null)
                return false;
            return a.Value == b.Value;
        }
        public static bool operator !=(Kontonummer a, Kontonummer b) => !(a == b);
    }
}
