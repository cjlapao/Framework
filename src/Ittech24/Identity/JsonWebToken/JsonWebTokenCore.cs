using Ittech24.Extensions;

namespace Ittech24.Identity.JsonWebToken
{
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

    public static class JsonWebTokenExtensions
    {


    }
}