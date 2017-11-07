using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

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

        public static void CopyFrom(this object copy, object original)
        {
            if (original != null)
            {
                if(original.GetType() == copy.GetType())
                {
                    foreach (PropertyInfo prop in original.GetType().GetProperties())
                    {
                        if (prop.CanWrite)
                        {
                            copy.GetType().GetProperty(prop.Name).SetValue(copy, prop.GetValue(original));
                        }
                    }
                }
            }
        }
    }
}
