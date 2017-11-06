using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Ittech24.Extensions;

namespace Ittech24.OAuth
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
            var client = new HttpClient();
            var message = new HttpRequestMessage(_requestToken.HttpMethod, _requestToken.Url);
            message.Headers.OAuthAuthentication(_requestToken);
            var response = await client.SendAsync(message);
            if (response.IsSuccessStatusCode)
            {
                if(_requestToken.Callback != null)
                {
                    _token.ConsumerKey = _requestToken.ConsumerKey;
                    _token.ConsumerSecret = _requestToken.ConsumerSecret;
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
                    if(response.RequestMessage.RequestUri != null)
                    {
                        _token.AuthorizationUrl = response.RequestMessage.RequestUri;
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
