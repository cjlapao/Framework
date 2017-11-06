using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ittech24.OAuth
{
    public interface IOAuthToken
    {
        Token Request { get; }
        Token Response { get; }

        Task<Token> SendAsync();
    }
}
