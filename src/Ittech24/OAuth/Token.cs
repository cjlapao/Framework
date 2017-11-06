using System;
using System.Collections.Generic;
using System.Net.Http;
using Ittech24.Extensions;

namespace Ittech24.OAuth
{
    public class Token : IToken
    {
        private string _consumerKey;
        private string _accessToken;
        private string _timestamp;
        private string _nonce;
        private string _version;
        private string _signaturemethod;
        private string _realm;
        private string _verifier;
        private IDictionary<string,string> _query;
        private IDictionary<string, string> _parameters;
        private string _callback;
        private Uri _absoluteUrl;

        public Token()
        {
            CreatedOn = DateTime.Now;
            Timestamp = DateTimeUtils.Timestamp().ToString();
            Nonce = StringUtils.Nonce();
            Version = "1.0";
            SignatureMethod = "HMAC-SHA1";
        }

        public string ConsumerKey
        {
            get { return _consumerKey; }
            set
            {
                _consumerKey = value;
                _parameters.Append("oauth_consumer_key", _consumerKey);
            }
        }

        public string ConsumerSecret { get; set; }

        public string AccessToken
        {
            get { return _accessToken; }
            set
            {
                _accessToken = value;
                _parameters.Append("oauth_token", _accessToken);
            }
        }

        public string AccessTokenSecret { get; set; }

        public string Timestamp
        {
            get { return _timestamp; }
            set
            {
                _timestamp = value;
                Parameters.Append("oauth_timestamp", _timestamp);
            }
        }

        public string Nonce
        {
            get { return _nonce; }
            set
            {
                _nonce = value;
                Parameters.Append("oauth_nonce", _nonce);
            }
        }

        public string Version
        {
            get { return _version; }
            set
            {
                _version = value;
                Parameters.Append("oauth_version", _version);
            }
        }

        public string Signature { get; set; }

        public string SignatureMethod
        {
            get { return _signaturemethod; }
            set
            {
                _signaturemethod = value;
                Parameters.Append("oauth_signature_method", _signaturemethod);
            }
        }

        public string Realm
        {
            get { return _realm; }
            set
            {
                _realm = value;
                Parameters.Append("realm", _realm);
            }
        }

        public string Verifier
        {
            get { return _verifier; }
            set
            {
                _verifier = value;
                Parameters.Append("oauth_verifier", _verifier);
            }
        }

        public Uri AuthorizationUrl { get; set; }

        public int ExpiresIn { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool Valid { get; set; }

        public string ApplicationID { get; set; }

        public string UserID { get; set; }

        public Uri Url { get; set; }

        public HttpMethod HttpMethod { get; set; } = HttpMethod.Get;

        public Uri AbsoluteUrl
        {
            get
            {
                if (Url != null)
                {
                    if (Query.Count > 0)
                    {
                        _absoluteUrl = new Uri(Url.AbsoluteUri +"?"+ Query.NormalizeParameters());
                    }
                    else
                    {
                        _absoluteUrl = new Uri(Url.AbsoluteUri);
                    }
                }
                return _absoluteUrl;
            }
        }

        public IDictionary<string, string> Parameters
        {
            get
            {
                if(_parameters == null)
                {
                    _parameters = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }
                return _parameters;
            }
            set
            {
                _parameters = value;
            }
        }

        public IDictionary<string, string> Query
        {
            get
            {
                if(_query == null)
                {
                    _query = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }
                return _query;
            }
        }

        public TokenException Exception { get; set; }

        public string Callback
        {
            get { return _callback; }
            set
            {
                _callback = value;
                if (!string.IsNullOrEmpty(_callback))
                {
                    Parameters.Append("oauth_callback",Uri.EscapeDataString(_callback));
                }
            }
        }

        public void LoadFromFile()
        {
            throw new NotImplementedException();
        }

        public void SaveToFile()
        {
            throw new NotImplementedException();
        }

        public void Sign() => Signature = OAuth.Signature.Generate(this);
    }
}
