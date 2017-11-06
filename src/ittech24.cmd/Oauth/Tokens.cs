using System;
using System.Collections.Generic;
using System.Text;
using Ittech24.Extensions;
using Ittech24.OAuth;
using System.Net.Http;
using System.Threading.Tasks;

namespace ittech24.cmd
{
    public class RequestToken
    {
        private string consumerKey;
        private string consumerSecret;
        private Uri url;
        private string callback;

        public RequestToken(string consumerKey, string consumerSecret,
            string url, string callback = null)
        {
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
            this.url = new Uri(url);
            if(callback != null)
            {
                this.callback = callback;
            }
        }

        public async Task<Token> Get()
        {
            var token = new Token
            {
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                Url = url,
                Callback = callback
            };
            token.Sign();
            var request = new Ittech24.OAuth.RequestToken(token);
            return await request.SendAsync();
        }
    }

    public class AuthorizationToken
    {
        private Token requestToken;
        private Uri url;

        public AuthorizationToken(Token token, string url)
        {
            requestToken = token;
            this.url = new Uri(url);
        }

        public async Task<Token> Get()
        {
            var token = new Token
            {
                ConsumerKey = requestToken.ConsumerKey,
                ConsumerSecret = requestToken.ConsumerSecret,
                AccessToken = requestToken.AccessToken,
                AccessTokenSecret = requestToken.AccessTokenSecret,
                Url = url
            };
            token.Sign();
            var access = new Ittech24.OAuth.AuthorizationToken(token);
            return await access.SendAsync();
        }
    }
}
