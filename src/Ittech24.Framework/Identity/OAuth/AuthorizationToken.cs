using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Ittech24.Framework.Extensions;

namespace Ittech24.Framework.OAuth
{
    public class AuthorizationToken: IOAuthToken
    {
        private Token _requestToken;
        private Token _token;

        public Token Request => _requestToken;

        public Token Response => _token;

        public AuthorizationToken(Token token)
        {
            _requestToken = token;
        }

        public async Task<Token> SendAsync()
        {
            _token = new Token();
            _token.Duplicate(_requestToken);
            _token.HttpMethod = HttpMethod.Post;
            var client = new HttpClient();
            var message = new HttpRequestMessage(_token.HttpMethod, new Uri($"{_token.Url.AbsoluteUri}?oauth_token={_token.AccessToken}"));
            var response = await client.SendAsync(message);
            _token.StatusCode = response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                _token.AuthorizationUrl = response.RequestMessage.RequestUri;
            }
            else
            {
                _token.Exception = new TokenException(response);
            }
            return _token;
        }
    }
}
