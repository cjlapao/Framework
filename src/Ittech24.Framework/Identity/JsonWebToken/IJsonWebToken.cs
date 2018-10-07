using System.Security.Cryptography.X509Certificates;

namespace Ittech24.Framework.Identity.JsonWebToken
{
    public interface IJsonWebToken
    {
        JWTAlgorithm Algorithm { get; set; }
        X509Certificate2 Certificate { get; set; }
        JoseHeader Header { get; set; }
        string RawHeader { get; set; }
        string EncodedHeader { get; set; }
        object Payload { get; set; }
        string RawPayload { get; set; }
        string EncodedPayload { get; set; }
        string Key { get; set; }
        string Token { get; set; }
        bool IsValid { get; set; }
                
        void Sign();
        string Encode();
        void Decode();
        void Decode(bool verify);
        bool Verify();
    }
}