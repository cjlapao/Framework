using System.Net.Http;
using System.Net.Http.Headers;
using Ittech24.Framework.OAuth;
using Ittech24.Framework.Extensions;

namespace Ittech24.Framework.OAuth
{
    public class TokenException : ITokenException
    {
        private string _message;
        private string _error;
        private string _statuscode;
        private string _responsephrase;
        private HttpHeaders _headers;

        public string Message => _message;

        public string Error => _error;

        public string StatusCode => _statuscode;

        public string ResponsePhrase => _responsephrase;

        public HttpHeaders Headers => _headers;

        public TokenException(HttpResponseMessage response)
        {
            var str = response.Content.ReadAsStringAsync().Result.SplitParameters();
            foreach(var kvp in str)
            {
                _message += kvp.Key;
                _error += kvp.Value;
            }
            _statuscode = response.StatusCode.ToString();
            _responsephrase = response.ReasonPhrase;
            _headers = response.Headers;
        }

        public TokenException(string message, string error)
        {
            _message = message;
            _error = error;
        }
    }
}
