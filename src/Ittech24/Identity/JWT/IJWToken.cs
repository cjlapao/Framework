using System.Security.Cryptography.X509Certificates;

namespace Ittech24.Identity.JWT
{
    public interface IJWToken
    {
        JoseHeader Header {get;set;}
        object Payload {get;set;}
        X509Certificate2 Certificate {get;set;}
        void Sign();
        void Sign(JWTAlgorithm algorithm);
    }
}