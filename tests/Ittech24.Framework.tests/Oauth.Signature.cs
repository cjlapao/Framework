using System;
using Xunit;
using Ittech24.Framework.OAuth;
using System.Collections.Generic;
using System.Net.Http;
using Moq;

namespace ittech24.tests
{
    public class OauthSignature
    {
        [Fact]
        public void CanGenerateSignature()
        {
            //creating the Oauth1.0a test token
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
            //setting the consumer secret and token secret to sign
            string ConsumerKey = "kd94hf93k423kf44";
            string tokenKey = "pfkkdhi9sl3r4s00";
            //setting the test signature result to compare
            string validSignature = "tR3+Ty81lMeYAr/Fid0kMTYa/WM=";
            //generating the signature for test
            string signature = Signature.Generate(HttpMethod.Get, url, ConsumerKey, parameters, tokenKey);
            //asserting if the signature matches with the one on the documentation
            Assert.Equal(validSignature, signature);
        }

        [Fact]
        public void CanSignToken()
        {
            //Creating a test token with the OAuth1.0a test data
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
            //signing the token
            token.Sign();
            //setting the test signature result to compare
            string validSignature = "tR3+Ty81lMeYAr/Fid0kMTYa/WM=";
            //asserting if the signature matches with the one on the documentation
            Assert.Equal(validSignature, token.Signature);
        }

        [Fact]
        public void CanNormalizeParameters()
        {
            //creating the Oauth1.0a test token
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
            //unescaping the OAuth1.0a base string for comparing
            string validNormalizedParameters = Uri.UnescapeDataString("file%3Dvacation.jpg%26oauth_consumer_key%3Ddpf43f3p2l4k3l03%26oauth_nonce%3Dkllo9940pd9333jh%26oauth_signature_method%3DHMAC-SHA1%26oauth_timestamp%3D1191242096%26oauth_token%3Dnnch734d00sl2jdk%26oauth_version%3D1.0%26size%3Doriginal");
            //normalizing the sent parameters
            string normalizedParameters = parameters.NormalizeParameters();
            //checking if the parameters are sorted and on the right position
            Assert.Equal(validNormalizedParameters, normalizedParameters);
        }
    }
}
