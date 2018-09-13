using System;
using System.Collections.Generic;
using System.Text;

namespace Ittech24.Security.Cryptography
{
    public enum HMACAlgorithm
    {
        HS256,
        HS384,
        HS512
    }

    class CryptographyCore
    {
    }

    public class HMAC
    {
        public static void Encode(Func<byte[], byte[], byte[]> func)
        {

        }
    }
}
