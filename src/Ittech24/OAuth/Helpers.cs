using System;
using System.Collections.Generic;
using System.Text;

namespace Ittech24.OAuth
{
    public static class Helpers
    {
        public static string NormalizeParameters(this IDictionary<string,string> parameters)
        {
            //sorting the dictionary to normalize the parameters
            var sorted = new SortedDictionary<string, string>();
            foreach (var kvp in parameters)
            {
                sorted.Add(kvp.Key, Uri.UnescapeDataString(kvp.Value));
            }
            StringBuilder sb = new StringBuilder();
            int i = 0;
            //unescaping the parameters to escape them on the string builder
            foreach (var kvp in sorted)
            {
                if (i > 0)
                {
                    sb.Append("&");
                }
                sb.AppendFormat("{0}={1}", kvp.Key, kvp.Value);
                i++;
            }
            return sb.ToString();
        }
    }
}
