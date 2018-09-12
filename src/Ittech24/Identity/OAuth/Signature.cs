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

        public static string Generate(IToken token)
        {
            //Creating a temporary parameter dictionary containing all the parameters and queries
            //from the token to construct the base string to sign
            var parameters = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if(token?.Parameters?.Count > 0)
            {
                foreach (var kvp in token.Parameters)
                {
                    parameters.Add(kvp.Key, kvp.Value);
                }
            }
            if(token?.Query?.Count > 0)
            {
                foreach(var kvp in token.Query)
                {
                    parameters.Add(kvp.Key, kvp.Value);
                }
            }
            //creating the signatue base string for signing
            StringBuilder sb = new StringBuilder();
            //getting the token http method
            sb.Append(token.HttpMethod.ToString().ToUpper());
            //getting the token url
            sb.Append($"&{Uri.EscapeDataString(token.Url.AbsoluteUri)}");
            //getting the concatenated dictionary containing all the queries and the tokens
            sb.Append($"&{Uri.EscapeDataString(parameters.NormalizeParameters())}");
            //generating the base string for signing
            string signatureBase = sb.ToString();
            //generating the signature key from the token
            string signatureKey = string.Format("{0}&{1}", token.ConsumerSecret, token.AccessTokenSecret);
            //encrypting the base string with the generated key
            HMACSHA1 hmca = new HMACSHA1(Encoding.ASCII.GetBytes(signatureKey));
            return Convert.ToBase64String(hmca.ComputeHash(Encoding.ASCII.GetBytes(signatureBase)));
        }
    }
}
