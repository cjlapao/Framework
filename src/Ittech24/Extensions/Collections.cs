using System;
using System.Collections.Generic;
using System.Text;

namespace Ittech24.Extensions
{
    public static class Collections
    {
        public static void Append<TKey,TValue>(this IDictionary<TKey,TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public static IDictionary<string,string> ExtractParameters(this string[] array, string separator = "=")
        {
            var result = new SortedDictionary<string, string>();
            foreach(string str in array)
            {
                if (str.Contains(separator))
                {
                    var kvp = str.Split(separator);
                    result.Add(kvp[0], kvp[1]);
                }
            }
            return result;
        }

        public static IDictionary<string, string> SplitParameters(this string value, string splitchar = "&", 
            string separator = "=")
        {
            var result = new SortedDictionary<string, string>();
            var strarr = value.Split(splitchar);
            foreach (string str in strarr)
            {
                if (str.Contains(separator))
                {
                    var kvp = str.Split(separator);
                    result.Add(kvp[0], kvp[1]);
                }
            }
            return result;
        }
    }
}
