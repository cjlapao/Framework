using System.Threading.Tasks;
using System.Net.Http;
using Ittech24.Framework.Extensions;

namespace Ittech24.Framework.OAuth
{
    public class RequestToken: IOAuthToken
    {
        private Token _requestToken;
        private Token _token;

        public Token Request => _requestToken;

        public Token Response => _token;

        public RequestToken(Token token)
        {
            _requestToken = token;
        }

        public async Task<Token> SendAsync()
        {
            _token = new Token();
            var client = new HttpClient();
            var message = new HttpRequestMessage(_requestToken.HttpMethod, _requestToken.AbsoluteUrl);
            message.Headers.OAuthAuthentication(_requestToken);
            var response = await client.SendAsync(message);
            _token.StatusCode = response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                _token.ConsumerKey = _requestToken.ConsumerKey;
                _token.ConsumerSecret = _requestToken.ConsumerSecret;
                _token.Callback = _requestToken.Callback;
                var str = await response.Content.ReadAsStringAsync();
                var parameters = str.SplitParameters();
                if(parameters.Count > 0)
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
