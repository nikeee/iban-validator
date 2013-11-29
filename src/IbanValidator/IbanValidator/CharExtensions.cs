
namespace IbanValidator
{
    internal static class CharExtensions
    {
#if NET20
        public static bool IsValidChar(char c)
#else
        public static bool IsValidChar(this char c)
#endif
        {
            return c >= 'A' && c <= 'Z';
        }
        
#if NET20
        public static int GetNumericValue(char c)
#else
        public static int GetNumericValue(this char c)
#endif
        {
            if (IsValidChar(c))
                return (int)c - 55;
            return int.Parse(c.ToString());
        }
    }
}
