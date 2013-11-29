using System.Text.RegularExpressions;

namespace IbanValidator
{
    internal static class StringExtensions
    {
#if NET20
        public static string StripWhiteSpace(string text)
#else
        public static string StripWhiteSpace(this string text)
#endif
        {
            return Regex.Replace(text, @"\s+", "");
        }
    }
}
