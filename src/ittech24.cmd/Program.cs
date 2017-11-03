using System;
using System.Collections.Generic;
using System.Net.Http;
using Ittech24.OAuth;

namespace ittech24.cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("oauth_consumer_key", "dpf43f3p2l4k3l03");
            parameters.Add("oauth_token", "nnch734d00sl2jdk");
            parameters.Add("oauth_signature_method", "HMAC-SHA1");
            parameters.Add("oauth_timestamp", "1191242096");
            parameters.Add("oauth_nonce", "kllo9940pd9333jh");
            parameters.Add("oauth_version", "1.0");
            parameters.Add("size", "original");
            parameters.Add("file", "vacation.jpg");
            var url = new Uri("http://photos.example.net/photos");
            Console.Write(Signature.Generate(HttpMethod.Get, url, "kd94hf93k423kf44&pfkkdhi9sl3r4s00", parameters));
        }
    }
}
