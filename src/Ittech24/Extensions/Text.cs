using System;
using System.Collections.Generic;
using System.Text;

namespace Ittech24.Extensions
{
    public static class TextExtensions
    {
        public static string GetString(this byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        public static string GetString(this byte[] bytes)
        {
            return bytes.GetString(Encoding.UTF8);
        }

        public static byte[] GetBytes(this string input, Encoding encoding)
        {
            return encoding.GetBytes(input);
        }

        public static byte[] GetBytes(this string input)
        {
            return input.GetBytes(Encoding.UTF8);
        }

        public static string Base64UrlEncode(this string input)
        {
            return input.GetBytes().Base64UrlEncode();
        }

        public static string Base64UrlEncode(this string input, Encoding encoding)
        {
            return input.GetBytes(encoding).Base64UrlEncode();
        }

        public static string Base64UrlEncode(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes)
                .Split("=")[0]
                .Replace('+', '-')
                .Replace('/', '_');
        }

        public static byte[] Base64UrlDecode(this string input)
        {
            string output = input;
            output = output
                .Replace("-", "+")
                .Replace("_", "/");
            switch (output.Length % 4)
            {
                case 0: break;
                case 2: output += "=="; break;
                case 3: output += "="; break;
                default: throw new ArgumentException("Illegal base64url string");
            }
            byte[] converted = Convert.FromBase64String(output);
            return converted;
        }
    }
}
