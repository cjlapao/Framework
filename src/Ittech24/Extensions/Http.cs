using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Headers;
using Ittech24.OAuth;

namespace Ittech24.Extensions
{
    public static partial class HttpExtensions
    {
        public static void OAuthAuthentication(this HttpHeaders headers, 
            IDictionary<string, string> parameters, string signature)
        {
            var sb = new StringBuilder();
            sb.Append("OAuth ");
            int i = 0;
            foreach(var kvp in parameters)
            {
                if(i >0)
                {
                    sb.Append(",");
                }
                sb.Append($"{kvp.Key}=\"{kvp.Value}\"");
                i++;
            }
            sb.Append($",oauth_signature=\"{Uri.EscapeDataString(signature)}\"");
            headers.Add("Authorization", sb.ToString());
        }

        public static void OAuthAuthentication(this HttpHeaders headers, IToken token)
        {
            var sb = new StringBuilder();
            sb.Append("OAuth ");
            int i = 0;
            foreach (var kvp in token.Parameters)
            {
                if (i > 0)
                {
                    sb.Append(",");
                }
                sb.Append($"{kvp.Key}=\"{kvp.Value}\"");
                i++;
            }
            if (string.IsNullOrEmpty(token.Signature))
                token.Sign();
            sb.Append($",oauth_signature=\"{Uri.EscapeDataString(token.Signature)}\"");
            headers.Add("Authorization", sb.ToString());
        }
    }
}
