using System.Globalization;

#if NET45

using System.Runtime.CompilerServices;

#endif

namespace IbanValidator
{
    internal static class CharExtensions
    {
#if NET20
        public static bool IsValidChar(char c)
#else
#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool IsValidChar(this char c)
#endif
        {
            return c >= 'A' && c <= 'Z';
        }
        
#if NET20
        public static int GetNumericValue(char c)
#else
#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static int GetNumericValue(this char c)
#endif
        {
            if (IsValidChar(c))
                return c - 55;
            return int.Parse(c.ToString(CultureInfo.InvariantCulture));
        }
    }
}
