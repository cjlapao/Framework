using System;
using System.Collections.Generic;
using System.Net.Http;
using Ittech24.OAuth;
using Ittech24.Extensions;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using static System.Console;

namespace ittech24.cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateJWS(string.Empty);
            ReadLine();
            var commands = ReadCommands(args);
            if (commands.Contains("sign"))
            {
                SignTest();
            }
            else if (commands.Contains("gettoken"))
            {
                GetToken(args);
            }
            else if (commands.Contains("jwt"))
            {
                GenerateJWS(string.Empty);
            }
            else if (commands.Contains("authtoken"))
            {
                var parameters = ReadParameters(args);
                if (!parameters.ContainsKey("consumerkey"))
                {
                    Console.Write("Please provide the consumer key");
                }
                else if (!parameters.ContainsKey("consumersecret"))
                {
                    Console.Write("Please provide the consumer secret");
                }
                else if (!parameters.ContainsKey("url"))
                {
                    Console.Write("Please provide the api Url");
                }
                else if (!parameters.ContainsKey("authurl"))
                {
                    Console.Write("Please provide the authentication Url");
                }
                else
                {
                    string callback = null;
                    if (parameters.ContainsKey("callback"))
                        callback = parameters["callback"];

                    var request = new RequestToken(parameters["consumerkey"], parameters["consumersecret"], parameters["url"], callback);
                    var requestToken = request.Get().Result;
                    var auth = new AuthorizationToken(requestToken,parameters["authurl"]);
                    var authToken = auth.Get().Result;
                    if(authToken.Exception == null)
                    {
                        Console.Write("authorization_url:" + authToken.AuthorizationUrl + "\r\n");
                        Console.Write("oauth_token: " + authToken.AccessToken + "\r\n");
                        Console.Write("oauth_token_secret: " + authToken.AccessTokenSecret + "\r\n");
                    }
                    else
                    {
                        Console.Write("error retrieving the token");
                    }
                }
            }
            else if (commands.Contains("accesstoken"))
            {
                var parameters = ReadParameters(args);
                if (!parameters.ContainsKey("consumerkey"))
                {
                    Console.Write("Please provide the consumer key");
                }
                else if (!parameters.ContainsKey("consumersecret"))
                {
                    Console.Write("Please provide the consumer secret");
                }
                if (!parameters.ContainsKey("accesstoken"))
                {
                    Console.Write("Please provide the access token");
                }
                else if (!parameters.ContainsKey("accesstokensecret"))
                {
                    Console.Write("Please provide the access token secret");
                }
                else if (!parameters.ContainsKey("verifier"))
                {
                    Console.Write("Please provide the verifier code");
                }
                else if (!parameters.ContainsKey("accessurl"))
                {
                    Console.Write("Please provide the access Url");
                }
                else
                {
                    var token = new Token
                    {
                        ConsumerKey = parameters["consumerkey"],
                        ConsumerSecret = parameters["consumersecret"],
                        AccessToken = parameters["accesstoken"],
                        AccessTokenSecret = parameters["accesstokensecret"],
                    };
                    var authToken = new AccessToken(token,parameters["accessurl"],parameters["verifier"]).Get().Result;
                    if (authToken.Exception == null)
                    {
                        Console.Write("oauth_token: " + authToken.AccessToken);
                        Console.Write("oauth_token_secret: " + authToken.AccessTokenSecret);
                    }
                    else
                    {
                        Console.Write("error retrieving the token");
                    }
                }
            }
            else
            {
                Console.Write("Please use one command to execute");
            }

        }

        static IDictionary<string,string> ReadParameters(string[] args)
        {
            var parameters = new SortedDictionary<string, string>();
            var commands = new List<string>();
            foreach (string str in args)
            {
                if (str.Contains("="))
                {
                    var kvp = str.Split("=");
                    parameters.Add(kvp[0], kvp[1]);
                }
                else
                {
                    commands.Add(str);
                }
            }
            return parameters;
        }

        static IList<string> ReadCommands(string[] args)
        {
            var commands = new List<string>();
            foreach (string str in args)
            {
                if (!str.Contains("="))
                {
                    commands.Add(str);
                }
            }
            return commands;
        }

        static void SignTest()
        {
            var param = new SortedDictionary<string, string>();
            param.Add("oauth_consumer_key", "dpf43f3p2l4k3l03");
            param.Add("oauth_token", "nnch734d00sl2jdk");
            param.Add("oauth_signature_method", "HMAC-SHA1");
            param.Add("oauth_timestamp", "1191242096");
            param.Add("oauth_nonce", "kllo9940pd9333jh");
            param.Add("oauth_version", "1.0");
            param.Add("size", "original");
            param.Add("file", "vacation.jpg");
            var url = new Uri("http://photos.example.net/photos");

            //Creating a token
            Token token = new Token();
            token.ConsumerKey = "dpf43f3p2l4k3l03";
            token.ConsumerSecret = "kd94hf93k423kf44";
            token.AccessTokenSecret = "pfkkdhi9sl3r4s00";
            token.AccessToken = "nnch734d00sl2jdk";
            token.Timestamp = "1191242096";
            token.Nonce = "kllo9940pd9333jh";
            token.Query.Add("size", "original");
            token.Query.Add("file", "vacation.jpg");
            token.Url = new Uri("http://photos.example.net/photos");
            token.Sign();
            Console.Write("Normal signature: " + Ittech24.OAuth.Signature.Generate(HttpMethod.Get, url, "kd94hf93k423kf44", param, "pfkkdhi9sl3r4s00")+"\r\n");
            Console.Write("Token signature: " + token.Signature);
        }

        static void GetToken(string[] args)
        {
            var parameters = ReadParameters(args);
            if (!parameters.ContainsKey("consumerkey"))
            {
                Console.Write("Please provide the consumer key");
            }
            else if (!parameters.ContainsKey("consumersecret"))
            {
                Console.Write("Please provide the consumer secret");
            }
            else if (!parameters.ContainsKey("url"))
            {
                Console.Write("Please provide the api Url");
            }
            else
            {
                string consumerKey = parameters["consumerkey"];
                string consumerSecret = parameters["consumersecret"];
                Uri url = new Uri(parameters["url"]);
                var client = new HttpClient();
                Token token = new Token
                {
                    ConsumerKey = consumerKey,
                    ConsumerSecret = consumerSecret,
                    Url = url
                };
                token.Sign();
                var message = new HttpRequestMessage(token.HttpMethod, token.Url);
                message.Headers.OAuthAuthentication(token);
                var response = client.SendAsync(message).Result;
                if (response.IsSuccessStatusCode)
                {
                    var str = response.Content.ReadAsStringAsync().Result;
                    Console.Write(str);
                }
                else
                {
                    var str = response.Content.ReadAsStringAsync().Result;
                    Console.Write("Error getting token: "+str);
                }
            }
        }

        static void GenerateJWS(string body)
        {
            string alg = "HS512";
            string typ = "JWT";
            var jose = new { alg, typ };
            string joseJson = JsonConvert.SerializeObject(jose);
            var data = new
            {
                sub = "12345",
                name = "Test44444",
                iat = "54321"
            };
            string dataJson = JsonConvert.SerializeObject(data);
            string joseBase64 = joseJson.Base64UrlEncode();
            WriteLine($"Base64 JOSE: {joseBase64}");
            string dataBase64 = dataJson.Base64UrlEncode();
            WriteLine($"Base64 Data: {dataBase64}");
            string payload = joseBase64 + "." + dataBase64;
            WriteLine($"Payload: {payload}");
            byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);
            HMACSHA512 crypto = new HMACSHA512(Encoding.Default.GetBytes("TestKey"));
            byte[] signDataBytes = crypto.ComputeHash(payloadBytes);
            string signData = Encoding.UTF8.GetString(signDataBytes);
            WriteLine($"SignData: {signData}");
            string signDataBase64 = signDataBytes.Base64UrlEncode();
            WriteLine($"SignDataBase64: {signDataBase64}");
            string token = payload + "." + signDataBase64;
            WriteLine($"Token: {token}");
            Console.ReadLine();
        }
    }
}
