using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Ittech24.Framework.Extensions;

namespace Ittech24.Framework.OAuth
{
    public class AccessToken
    {
        private Token _requestToken;
        private Token _token;

        public Token Request => _requestToken;

        public Token Response => _token;

        public AccessToken(Token token)
        {
            _requestToken = token;
        }

        public async Task<Token> SendAsync()
        {
            _token = new Token();
            _token.Duplicate(_requestToken);
            _token.HttpMethod = HttpMethod.Post;
            _token.Sign();
            var client = new HttpClient();
            var message = new HttpRequestMessage(_token.HttpMethod, _token.AbsoluteUrl);
            message.Headers.OAuthAuthentication(_token);
            var response = await client.SendAsync(message);
            _token.StatusCode = response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var parameters = str.SplitParameters();
                if (parameters.Count > 0)
                {
                    if (parameters.ContainsKey("oauth_token"))
                    {
                        _token.AccessToken = parameters["oauth_token"];
                        _token.AccessTokenSecret = parameters["oauth_token_secret"];
                    }
                }
            }
            else
            {
                _token.Exception = new TokenException(response);
            }
            return _token;

        }
    }
}
