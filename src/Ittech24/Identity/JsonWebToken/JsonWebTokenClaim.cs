using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Ittech24.Extensions;
using Newtonsoft.Json;

namespace Ittech24.Identity.JsonWebToken
{
    [DataContract]
    public class JsonWebTokenClaim
    {
        [DataMember(Name = "jti")]
        public string JsonWebTokenId { get; set; }
        [DataMember(Name = "iss")]
        public string Issuer { get; set; }
        [DataMember(Name = "sub")]
        public string Subject { get; set; }
        [DataMember(Name = "aud")]
        public string Audience { get; set; }
        [DataMember(Name = "exp")]
        public int Expiration { get; set; }
        [DataMember(Name = "nbf")]
        public int NotBefore { get; set; }
        [DataMember(Name = "iat")]
        public int IssuedAt { get; set; }
        [JsonExtensionData]
        public Dictionary<string, object> ExtraProperties { get; set; } = new Dictionary<string, object>();

        public string ToJson()
        {
            IJsonSerializer jsonSerializer = new DefaultJsonSerializer();
            return jsonSerializer.Serialize(this);
        }

        public string Encode()
        {
            return ToJson().Base64UrlEncode();
        }

        public static void TryParse(string input, out JsonWebTokenClaim claim)
        {
            claim = null;
            IJsonSerializer jsonSerializer = new DefaultJsonSerializer();
            try
            {
                claim = jsonSerializer.Deserialize<JsonWebTokenClaim>(input);
            }
            catch { }
        }
    }
}
