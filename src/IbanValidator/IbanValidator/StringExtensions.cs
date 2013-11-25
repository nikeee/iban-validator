using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IbanValidator
{
    internal static class StringExtensions
    {
        public static string StripWhiteSpace(this string text)
        {
            return Regex.Replace(text, @"\s+", "");
        }
    }
}
