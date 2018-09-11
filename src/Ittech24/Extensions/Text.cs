using System;
using System.Collections.Generic;
using System.Text;

namespace Ittech24.Extensions
{
    public static class TextExtensions
    {
        public static string AddBase64Padding(this string value)
        {
            return value.PadRight(value.Length + (4 - value.Length % 4) % 4, '=');
        }
    }
}
