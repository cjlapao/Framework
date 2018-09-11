using System;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Newtonsoft.Json;
using Ittech24.Extensions;

namespace Ittech24.Identity.JWT 
{
    public class JWToken : IJWToken
    {
        public JWTAlgorithm Algorithm {get;set;} = JWTAlgorithm.HS256;
        public JoseHeader Header {get;set;}
        public object Payload {get;set;}
        public X509Certificate2 Certificate {get;set;}
        public string Key {get;set;}
        public string Signature { get; set; }
        public string Token { get; set; }

        public void Sign()
        {
            if (string.IsNullOrEmpty(Key))
                throw new ArgumentNullException("Key cannot be empty");

            byte[] keyBytes = Encoding.UTF8.GetBytes(Key);

            if (Header == null) {
                Header = BuildJoseHeader();
            }

            string payload = null;

            if (Payload.GetType() == typeof(JWTClaim))
            {
                payload = ((JWTClaim)Payload).ToBase64UrlEncode();
            }
            else
            {
                var test = JsonConvert.SerializeObject(Payload, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                payload = HttpExtensions.Base64UrlEncode(test);
            }

            StringBuilder payloadBase64 = new StringBuilder();
            payloadBase64.Append(Header.ToBase64UrlEncode());
            payloadBase64.Append(".");
            payloadBase64.Append(payload);

            StringBuilder token = new StringBuilder();
            token.Append(payloadBase64.ToString());
            token.Append(".");
            byte[] payloadBytes = Encoding.UTF8.GetBytes(payloadBase64.ToString());
            byte[] hashedBytes = null;
            switch (Algorithm)
            {
                case JWTAlgorithm.HS256:
                    HMACSHA256 encHMAC256 = new HMACSHA256(keyBytes);
                    hashedBytes = encHMAC256.ComputeHash(payloadBytes);
                    Signature = hashedBytes.Base64UrlEncode();
                    break;
                case JWTAlgorithm.HS384:
                    HMACSHA384 encHMAC384 = new HMACSHA384(keyBytes);
                    hashedBytes = encHMAC384.ComputeHash(payloadBytes);
                    Signature = hashedBytes.Base64UrlEncode();
                    break;
                case JWTAlgorithm.HS512:
                    HMACSHA512 encHMAC512 = new HMACSHA512(keyBytes);
                    hashedBytes = encHMAC512.ComputeHash(payloadBytes);
                    Signature = hashedBytes.Base64UrlEncode();
                    break;
                default: goto case JWTAlgorithm.HS256;
            }
            token.Append(Signature);
            Token = token.ToString();
        }

        public void Sign(JWTAlgorithm algorithm)
        {
            Algorithm = algorithm;
            Sign();
        }

        public void LoadCertificateFromFile(string filename)
        {
            if(!File.Exists(filename))
            {
                throw new FileNotFoundException("Certificate was not found");
            }
            // byte[] test 
            // using
            Certificate = new X509Certificate2();            
        }

        public bool Verify()
        {
            if (string.IsNullOrEmpty(Token))
                throw new ArgumentNullException("Token is missing or empty and cannot be verified.");
            if (string.IsNullOrEmpty(Key))
                throw new ArgumentNullException("Decrypting key is missing or empty, cannot check token.");

            string[] parts = Token.Split(".");
            if (parts.Count() < 2)
                throw new  ArgumentException("Token not correcty constructed.");
            string header = Encoding.UTF8.GetString(Convert.FromBase64String(parts[0].AddBase64Padding()));
            JoseHeader testHeader = null;
            try
            {
                testHeader = JsonConvert.DeserializeObject<JoseHeader>(header);
            }
            catch
            {
                return false;
            }
            if (testHeader == null || testHeader.Type == JWTType.NONE)
                return false;
            Header = testHeader;
            var r = parts[1].Length % 3;
            string payload = Encoding.UTF8.GetString(Convert.FromBase64String(parts[1].AddBase64Padding()));
            JWTClaim testClaim = JsonConvert.DeserializeObject<JWTClaim>(payload);
            var t2 = JsonConvert.DeserializeObject(payload);
            Payload = t2;
            Sign(testHeader.Algorithm);
            if (Signature == parts[2])
                return true;
            return false;
        }

        private JoseHeader BuildJoseHeader(bool generateKeyID = true){
            Header = new JoseHeader();
            Header.Algorithm = Algorithm;
            Header.Type = JWTType.JWS;
            if(Certificate !=null && generateKeyID){
                Header.KeyId = Certificate.SerialNumber;
            }
            return Header;
        }
    }
}