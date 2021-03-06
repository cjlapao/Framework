﻿using System.Net.Http.Headers;

namespace Ittech24.Framework.OAuth
{
    public interface ITokenException
    {
        string Message { get; }
        string Error { get; }
        string StatusCode { get; }
        string ResponsePhrase { get; }
        HttpHeaders Headers { get; }
    }
}
