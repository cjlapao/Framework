using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Linq;
using System.Security.Cryptography;

namespace Ittech24.OAuth
{
    public static class Signature
    {
        public static string Generate(HttpMethod httpMethod, Uri url,
            string consumerSecret,
            IDictionary<string,string> parameters,
            string tokenSecret = null)
        {
            //creating the signatue basestring
            StringBuilder sb = new StringBuilder();
            sb.Append(httpMethod.ToString().ToUpper());
            sb.Append($"&{Uri.EscapeDataString(url.AbsoluteUri)}");
            sb.Append($"&{Uri.EscapeDataString(parameters.NormalizeParameters())}");
            string signatureBase = sb.ToString();
            string signatureKey = string.Format("{0}&{1}",consumerSecret,tokenSecret);
            HMACSHA1 hmca = new HMACSHA1(Encoding.ASCII.GetBytes(signatureKey));
            return Convert.ToBase64String(hmca.ComputeHash(Encoding.ASCII.GetBytes(signatureBase)));
        }
    }
}
