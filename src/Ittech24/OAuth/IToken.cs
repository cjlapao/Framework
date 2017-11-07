using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Ittech24.OAuth
{
    public interface IToken
    {
        string ConsumerKey { get; }
        string ConsumerSecret { get; }
        string AccessToken { get; }
        string AccessTokenSecret { get; }
        string Timestamp { get; }
        string Nonce { get; }
        string Version { get; }
        string Signature { get; }
        string SignatureMethod { get; }
        string Realm { get; }
        string Verifier { get; }
        Uri AuthorizationUrl { get; }
        string Callback { get; }
        int ExpiresIn { get; }
        DateTime CreatedOn { get; }
        bool Valid { get; }
        string ApplicationID { get; }
        string UserID { get; }
        Uri Url { get; }
        HttpMethod HttpMethod { get; }
        Uri AbsoluteUrl { get; }
        TokenException Exception { get; }
        HttpStatusCode StatusCode { get; }
        bool IsSuccess { get; }
        IDictionary<string,string> Parameters { get; }
        IDictionary<string,string> Query { get; }

        void SaveToFile();
        void LoadFromFile();
        void Sign();
        void Duplicate(Token token);
    }
}
