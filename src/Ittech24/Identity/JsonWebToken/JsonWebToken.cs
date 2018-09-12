using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Newtonsoft.Json;
using Ittech24.Extensions;

namespace Ittech24.Identity.JsonWebToken 
{
    public class JsonWebToken : IJsonWebToken
    {
        private IJsonSerializer jsonSerializer = new DefaultJsonSerializer();
        private JoseHeader _joseHeader;

        public JWTAlgorithm Algorithm {get;set;} = JWTAlgorithm.HS256;
        public JoseHeader Header
        {
            get
            {
                if (_joseHeader != null)
                    Algorithm = _joseHeader.Algorithm;
                return _joseHeader;
            }
            set
            {
                _joseHeader = value;
            }
        }
        public object Payload {get;set;}
        public X509Certificate2 Certificate {get;set;}
        public string Key {get;set;}
        public string Signature { get; set; }
        public string Token { get; set; }
        public JsonWebTokenClaim PublicClaims { get; set; }

        public void LoadCertificateFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("Certificate was not found");
            }

            try
            {
                Certificate = new X509Certificate2(filename);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Invalid certificate, please load a valid file", ex);
            }
        }

        public void Sign()
        {
            string payload = "{}";
            StringBuilder payloadBase64 = new StringBuilder();
            StringBuilder token = new StringBuilder();

            if (string.IsNullOrEmpty(Key))
                throw new ArgumentNullException("Key cannot be empty");

            // Get encryption key
            byte[] keyBytes = Key.GetBytes();

            // Rebuild Jose Header if it is null
            if (Header == null) {
                Header = BuildJoseHeader();
            }

            // Detecting if the payload is a standard public claim payload
            // or if it contains a private payload
            if (Payload?.GetType() == typeof(JsonWebTokenClaim))
            {
                payload = (Payload as JsonWebTokenClaim).Encode();
            }
            else
            {
                string jsonPayload = jsonSerializer.Serialize(Payload ?? "{}");
                payload = jsonPayload.Base64UrlEncode();
            }

            payloadBase64.Append(Header.Encode());
            payloadBase64.Append(".");
            payloadBase64.Append(payload ?? string.Empty);

            token.Append(payloadBase64.ToString());
            token.Append(".");
            byte[] payloadBytes = payloadBase64.ToString().GetBytes();
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
                default:
                    // temporary impementation of a default encryption
                    Algorithm = JWTAlgorithm.HS256;
                    BuildJoseHeader();
                    goto case JWTAlgorithm.HS256;
            }
            token.Append(Signature);
            Token = token.ToString();
        }

        public void Sign(JWTAlgorithm algorithm)
        {
            Algorithm = algorithm;
            Sign();
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

            string headerJson = parts[0].Base64UrlDecode().GetString();
            string payloadJson = parts[1].Base64UrlDecode().GetString();
            string signatureJson = parts[2].Base64UrlDecode().GetString();

            JsonWebToken token = new JsonWebToken();
            token.Key = Key;

            JoseHeader.TryParse(headerJson, out JoseHeader header);
            if (header == null || header.Type == JWTType.NONE)
                return false;
            token.Header = header;

            JsonWebTokenClaim.TryParse(payloadJson, out JsonWebTokenClaim testClaim);
            if (testClaim != null)
                token.Payload = testClaim;
            else
                token.Payload = payloadJson;
            Dictionary<string, object> test1 = jsonSerializer.Deserialize<Dictionary<string, object>>(payloadJson);
            token.Payload = test1;
            token.Sign();
            if (token.Signature == parts[2])
                return true;
            return false;
        }

        private JoseHeader BuildJoseHeader(bool generateKeyID = true){
            Header = new JoseHeader();
            Header.Algorithm = Algorithm;
            Header.Type = JWTType.JWS;
            if(Certificate != null && generateKeyID){
                Header.KeyId = Certificate.SerialNumber;
            }
            return Header;
        }
    }
}