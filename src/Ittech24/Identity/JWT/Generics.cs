using System;

namespace Ittech24.Identity.JWT{
    public enum JWTAlgorithm {
        Empty,
        none,
        HS256,
        HS384,
        HS512,
        RS256
    }

    public enum JWTType
    {
        NONE,
        JWS,
        JWE,
        JWT
    }
}