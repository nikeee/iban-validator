using System.Text.RegularExpressions;

namespace IbanValidator
{
    internal static class StringExtensions
    {
#if NET20
        public static string StripWhiteSpace(string text) => Regex.Replace(text, @"\s+", "");
#else
        public static string StripWhiteSpace(this string text) => Regex.Replace(text, @"\s+", "");
#endif
    }
}
