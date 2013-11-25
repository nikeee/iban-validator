using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IbanValidator
{
    internal static class CharExtensions
    {
        public static bool IsValidChar(this char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        public static int GetNumericValue(this char c)
        {
            return c >= 'A' && c <= 'Z';
        }
    }
}
