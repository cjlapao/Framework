using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Newtonsoft.Json;
using Ittech24.Framework.Extensions;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Ittech24.Framework.Identity.JsonWebToken 
{
    public class JsonWebToken: IJsonWebToken
    {
        private IJsonSerializer jsonSerializer = new DefaultJsonSerializer();

        public JWTAlgorithm Algorithm
        {
            get
            {
                if (Header == null)
                {
                    Header = new JoseHeader();
                    Header.Algorithm = JWTAlgorithm.none;
                }
                return Header.Algorithm;
            }
            set
            {
                if (Header == null)
                    Header = new JoseHeader();
                Header.Algorithm = value;
            }
        }
        public X509Certificate2 Certificate { get; set; }
        public JoseHeader Header { get; set; }
        public string RawHeader { get; set; }
        public string EncodedHeader { get; set; }
        public object Payload { get; set; }
        public string RawPayload { get; set; }
        public string EncodedPayload { get; set; }
        public string Signature { get; set; }
        public string Key { get; set; }
        public string Token { get; set; }
        public bool IsValid { get; set; } = false;
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

        /// <summary>
        /// Signs the JsonWebtoken using the selected algorithm
        /// </summary>
        public void Sign()
        {
            SignAsync().Wait();
        }

        /// <summary>
        /// Sign asynchronously the JsonWebToken using the selected algorithm
        /// </summary>
        /// <returns></returns>
        public Task SignAsync()
        {
            var task = Task.Run(async () =>
            {
                DateTime startTime = DateTime.Now;
                if (string.IsNullOrEmpty(Key))
                    throw new ArgumentNullException("Key");

                if (Algorithm == JWTAlgorithm.Empty)
                    throw new ArgumentException("Algorithm cannot be set to empty, if you do not wish a signature set it up to none");

                if (string.IsNullOrEmpty(EncodedHeader))
                {
                    if (RawHeader != null || Header != null)
                        await EncodeAsync();
                    else
                        throw new ArgumentNullException("Header");
                }

                if (string.IsNullOrEmpty(EncodedPayload))
                {
                    if (RawPayload != null || Payload != null)
                        await EncodeAsync();
                    else
                        throw new ArgumentNullException("Payload");
                }

                // Get encryption key
                byte[] keyBytes = Key.GetBytes();

                StringBuilder payload = new StringBuilder();
                payload.Append(EncodedHeader);
                payload.Append(".");
                payload.Append(EncodedPayload);

                if (Algorithm != JWTAlgorithm.none)
                    Signature = HashSignature(Algorithm, keyBytes, payload.ToString().GetBytes()).Base64UrlEncode();
                TimeSpan ts = DateTime.Now.Subtract(startTime);
                Console.WriteLine($"=> Signing took: {ts.Milliseconds}");
            });
            return task;
        }

        /// <summary>
        /// Encodes the JsonWebToken
        /// </summary>
        public string Encode()
        {
            return EncodeAsync().Result;
        }

        /// <summary>
        /// Encodes asynchronously the JsonWebToken
        /// </summary>
        /// <returns></returns>
        public Task<string> EncodeAsync()
        {
            var task = Task.Run(async () =>
            {
                DateTime startTime = DateTime.Now;
                string headerJson = null;
                string payloadJson = null;
                if (Header != null)
                    headerJson = Header.ToJson();

                if (Payload != null)
                {
                    if (Payload is JsonWebTokenClaim)
                        payloadJson = (Payload as JsonWebTokenClaim).ToJson();
                    else
                        payloadJson = JsonConvert.SerializeObject(Payload);
                }
                else if (RawPayload != null)
                    payloadJson = RawPayload;
                else
                    payloadJson = JsonConvert.SerializeObject("{}");

                // Checking if the header is set up otherwise we will not be able to encode the token
                if (string.IsNullOrEmpty(headerJson))
                    throw new ArgumentNullException("Header");

                if (Algorithm == JWTAlgorithm.Empty)
                    throw new ArgumentException("Algorithm cannot be set to empty, if you do not wish a signature set it up to none");

                EncodedHeader = headerJson.Base64UrlEncode();
                EncodedPayload = payloadJson.Base64UrlEncode();

                await SignAsync();

                StringBuilder payload = new StringBuilder();
                payload.Append(EncodedHeader);
                payload.Append(".");
                payload.Append(EncodedPayload);
                payload.Append(".");
                payload.Append(Signature);
                Token = payload.ToString();
                TimeSpan ts = DateTime.Now.Subtract(startTime);
                Console.WriteLine($"=> Encode took: {ts.Milliseconds}");
                return Token;
            });
            return task;
        }

        /// <summary>
        /// Decodes a json string into a JsonWebToken object
        /// </summary>
        /// <param name="token">Json string representing the token</param>
        /// <returns>An <see cref="JsonWebToken"/> Object</returns>
        public static JsonWebToken Decode(string token)
        {
            return DecodeAsync(token, false).Result;
        }

        /// <summary>
        /// Decodes a json string into a JsonWebToken object
        /// </summary>
        /// <param name="token">Json string representing the token</param>
        /// <param name="verify">Should the token be verified</param>
        /// <returns>An <see cref="JsonWebToken"/> Object</returns>
        public static JsonWebToken Decode(string token, bool verify)
        {
            return DecodeAsync(token, verify).Result;
        }

        /// <summary>
        /// Decodes a json string asynchronously into a JsonWebToken object
        /// </summary>
        /// <param name="token">Json string representing the token</param>
        /// <returns>An <see cref="JsonWebToken"/> Object</returns>
        public static Task<JsonWebToken> DecodeAsync(string token)
        {
            return DecodeAsync(token, false);
        }

        /// <summary>
        /// Decodes a json string asynchronously into a JsonWebToken object
        /// </summary>
        /// <param name="token">Json string representing the token</param>
        /// <param name="verify">Should the token be verified</param>
        /// <returns>An <see cref="JsonWebToken"/> Object</returns>
        public static Task<JsonWebToken> DecodeAsync(string token, bool verify)
        {
            var task = Task.Run(async () =>
            {
                JsonWebToken result = new JsonWebToken
                {
                    Token = token
                };
                await result.DecodeAsync(verify);
                return result;
            });
            return task;
        }

        /// <summary>
        /// Decodes the string representation of this object
        /// </summary>
        public void Decode()
        {
            DecodeAsync(false).Wait();
        }

        /// <summary>
        /// Decodes the string representation of this object
        /// </summary>
        /// <param name="verify">Verifies if the token is valid</param>
        public void Decode(bool verify)
        {
            DecodeAsync(verify).Wait();
        }

        /// <summary>
        /// Decodes asynchronously the string representation of this object
        /// </summary>
        /// <returns></returns>
        public Task DecodeAsync()
        {
            return DecodeAsync(false);
        }

        /// <summary>
        /// Decodes asynchronously the string representation of this object
        /// </summary>
        /// <param name="verify">Verifies if the token is valid</param>
        /// <returns></returns>
        public Task DecodeAsync(bool verify)
        {
            var task = Task.Run(() =>
            {
                DateTime startTime = DateTime.Now;
                // Checking if token is empty, and raising an exception if true
                if (string.IsNullOrEmpty(Token))
                    throw new ArgumentNullException("Token");
                // breaking the token into the constituting parts and checking if there is any signature present
                // on a valid token we need to always have a three part token, even if the signature is 
                // not present there should always be  2 dots.
                string[] parts = Token.Split('.');
                if (parts.Count() < 3)
                    throw new ArgumentException("Token not correcty constructed.");
                // Decoding the header and the payload for verification and parse
                string decodedHeader = parts[0].Base64UrlDecode().GetString();
                string decodedPayload = parts[1].Base64UrlDecode().GetString();

                string signature = parts[2];
                // setting the raw header and raw payload properties values
                RawHeader = decodedHeader;
                RawPayload = decodedPayload;
                // trying to parse the header into a JoseHeader as expected
                JoseHeader.TryParse(decodedHeader, out JoseHeader header);
                Header = header;
                // Trying to parse the payload into a JWT Claim, if this is not the case we will store the raw data and
                // a string as a payload to later be analized
                JsonWebTokenClaim.TryParse(decodedPayload, out JsonWebTokenClaim claim);
                if (claim != null)
                    Payload = claim;
                else
                    Payload = decodedPayload;
                //Checking if we can verify the token
                if ((Header?.Algorithm != JWTAlgorithm.Empty || Header?.Algorithm != JWTAlgorithm.none)
                    && !string.IsNullOrEmpty(signature))
                {
                    if (verify)
                        Verify();
                }
                TimeSpan ts = DateTime.Now.Subtract(startTime);
                Console.WriteLine($"=> Decode took: {ts.Milliseconds}");
            });
            return task;
        }

        /// <summary>
        /// Verifies if this token is valid using the token signature
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public bool Verify()
        {
            IsValid = Verify(Token, Key);
            return IsValid;
        }

        /// <summary>
        /// Verifies asynchronously if this token is valid using the token signature
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public Task<bool> VerifyAsync()
        {
            var task = Task.Run(async () =>
            {
                IsValid = await VerifyAsync(Token, Key);
                return IsValid;
            });
            return task;
        }

        /// <summary>
        /// Verifies if this token is valid using the token signature
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public static bool Verify(string token, string key)
        {
            return VerifyAsync(token, key).Result;
        }

        /// <summary>
        /// Verifies asynchronously if this token is valid using the token signature
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public static Task<bool> VerifyAsync(string token, string key)
        {
            var task = Task.Run(() =>
            {
                DateTime startTime = DateTime.Now;
                if (string.IsNullOrEmpty(token))
                    throw new ArgumentNullException("Token");

                if (string.IsNullOrEmpty(key))
                    throw new ArgumentNullException("Key");

                string[] parts = token.Split('.');
                if (parts.Count() < 3)
                    throw new ArgumentException("Token not correcty constructed.");

                string headerJson = parts[0].Base64UrlDecode().GetString();
                string payloadJson = parts[1].Base64UrlDecode().GetString();
                string signatureJson = parts[2].Base64UrlDecode().GetString();

                Dictionary<string, object> header = null;
                try
                {
                    header = JsonConvert.DeserializeObject<Dictionary<string, object>>(headerJson);
                    if (header == null)
                        return false;
                }
                catch
                {
                    return false;
                }

                JWTAlgorithm algorithm = JWTAlgorithm.Empty;
                if (header.ContainsKey("alg"))
                    Enum.TryParse((string)header["alg"], out algorithm);
                else
                    return false;

                string signatureDecoded = HashSignature(algorithm, key.GetBytes(), $"{parts[0]}.{parts[1]}".GetBytes()).Base64UrlEncode();
                TimeSpan ts = DateTime.Now.Subtract(startTime);
                Console.WriteLine($"=> Verifying took: {ts.Seconds}");

                if (signatureDecoded == parts[2])
                    return true;
                return false;
            });
            return task;
        }

        /// <summary>
        /// Hash a token signature
        /// </summary>
        /// <param name="algorithm"><see cref="JWTAlgorithm"/> to encrypt</param>
        /// <param name="keyBytes">Key</param>
        /// <param name="payloadBytes">Payload</param>
        /// <returns>Hashed Signature</returns>
        public static byte[] HashSignature(JWTAlgorithm algorithm, byte[] keyBytes, byte[] payloadBytes)
        {
            byte[] hashedBytes = null;
            switch (algorithm)
            {
                case JWTAlgorithm.HS256:
                    HMACSHA256 encHMAC256 = new HMACSHA256(keyBytes);
                    hashedBytes = encHMAC256.ComputeHash(payloadBytes);
                    break;
                case JWTAlgorithm.HS384:
                    HMACSHA384 encHMAC384 = new HMACSHA384(keyBytes);
                    hashedBytes = encHMAC384.ComputeHash(payloadBytes);
                    break;
                case JWTAlgorithm.HS512:
                    HMACSHA512 encHMAC512 = new HMACSHA512(keyBytes);
                    hashedBytes = encHMAC512.ComputeHash(payloadBytes);
                    break;
                default:
                    // temporary impementation of a default encryption
                    goto case JWTAlgorithm.HS256;
            }
            return hashedBytes;
        }

    }
}