using Ittech24.Framework.Extensions;

namespace Ittech24.Framework.Identity.JsonWebToken
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

    public static class JWTPublicRegistry
    {
        public const string Issuer = "iss";
        public const string Subject = "sub";
        public const string Audience = "aud";
        public const string ExpirationTime = "exp";
        public const string NotBefore = "nbf";
        public const string IssuedAt = "iat";
        public const string JWTId = "jti";
        public const string Name = "name";
        public const string GivenName = "given_name";
        public const string Surname = "family_name";
        public const string MiddleNames = "middle_name";
        public const string Nickname = "nickname";
        public const string ShorthandName = "preferred_username";
        public const string ProfilePageUrl = "profile";
        public const string ProfilePictureUrl = "picture";
        public const string Website = "website";
        public const string Email = "email";
        public const string Gender = "gender";
        public const string Birthday = "birthdate";
        public const string Timezone = "zoneinfo";
        public const string Locale = "locale";
        public const string PhoneNumber = "phone_number";
        public const string PhoneNumberVerified = "phone_number_verified";
        public const string Address = "address";
        public const string UpdatedAt = "updated_at";
        public const string AuthorizedParty = "azp";
        public const string ClientSession = "nonce";
        public const string AuthenticationTime = "auth_time";
        public const string AccessTokenHash = "at_hash";
        public const string CodeHash = "c_hash";
        public const string AuthenticationContext = "acr";
        public const string AuthenticationMethod = "amr";
        public const string PublicKeyOpenID = "sub_jwk";
        public const string Confirmation = "cnf";
        public const string OriginatingIdentityString = "orig";
        public const string DestinationIdentityString = "dest";
        public const string MediaKeyFingerprint = "mky";
        public const string SecurityEvents = "events";
        public const string TimeOfEvent = "toe";
        public const string TransactionIdentifier = "txn";
        public const string ResourcePriorityHeaderAuthorization = "rph";
        public const string SessionID = "sid";
        public const string VectorOfTrust = "vot";
        public const string VectorOfTrustUrl = "vtm";
    }

    public static class JoseJWSPublicRegistry
    {
        public const string Algorithm = "alg";
        public const string JWKSetUrl = "jku";
        public const string JsonWebKey = "jwk";
        public const string KeyID = "kid";
        public const string X509Url = "x5u";
        public const string X509CertificateChain = "x5c";
        public const string X509CertificateSHA1Thumbprint = "x5t";
        public const string X509CertificateSHA256Thumbprint = "x5t#256";
        public const string Type = "typ";
        public const string ContentType = "cty";
        public const string Critical = "crit";
    }
}